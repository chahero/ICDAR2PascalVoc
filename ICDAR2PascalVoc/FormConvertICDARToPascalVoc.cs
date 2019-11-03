using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace ICDAR2PascalVoc
{
    public partial class FormConvertICDARToPascalVoc : Form
    {
        public FormConvertICDARToPascalVoc()
        {
            InitializeComponent();

            // listViewFiles 설정
            listViewFiles.View = View.Details;
            listViewFiles.GridLines = true;
            listViewFiles.FullRowSelect = true;
            listViewFiles.HeaderStyle = ColumnHeaderStyle.Clickable;
            listViewFiles.CheckBoxes = true;

            listViewFiles.Columns.Add("v", 40);
            listViewFiles.Columns.Add("file name", 150);
            listViewFiles.Columns.Add("directory", 200);
        }
        private void listViewFiles_DragEnter(object sender, DragEventArgs e)
        {
            bool bDesignatedFileExist = false;

            // 여러 파일 리스트를 가져옴
            string[] strFiles = (string[])(e.Data.GetData(DataFormats.FileDrop, false));

            // 여러 파일 중 하나라도 xml 파일이 있는지 확인
            foreach (string strFile in strFiles)
            {
                string strFileExtension = Path.GetExtension(strFile);
                if (strFileExtension.ToLower() == ".txt")
                {
                    bDesignatedFileExist = true;
                }
            }

            // 여러 파일 중 하나라도 xml 파일이 있으면 copy 표시
            if (bDesignatedFileExist == true)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void listViewFiles_DragDrop(object sender, DragEventArgs e)
        {           
            string[] strFiles = (string[])(e.Data.GetData(DataFormats.FileDrop, false));

            // listview files에 추가
            AddFilesIntoListView(strFiles, ref listViewFiles);
        }
        
        // File List를 지정한 ListView에 추가
        private void AddFilesIntoListView(string[] strFiles, ref ListView listView)
        {
            // 가져온 파일들에 대하여 처리
            foreach (string strFile in strFiles)
            {
                string strFileExtension = Path.GetExtension(strFile);
                string strFileName = Path.GetFileName(strFile);
                string strDirectoryName = Path.GetDirectoryName(strFile);

                ListViewItem lvItem = new ListViewItem();

                if (strFileExtension.ToLower() == ".txt")
                {
                    lvItem.SubItems.Add(strFileName);
                    lvItem.SubItems.Add(strDirectoryName);
                    listView.Items.Add(lvItem);
                }
                else
                {
                    continue;
                }
            }
        }


        private void listViewFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // 선택된 줄들의 모음 반환
                var varSelectedCollections = listViewFiles.SelectedIndices;

                for (int i = varSelectedCollections.Count - 1; i >= 0; i--)
                {
                    int iIndex = varSelectedCollections[i];
                    listViewFiles.Items[iIndex].Remove();
                }
            }
        }

        // listFileView에 있는 txt 파일 읽어서, PASCAL Voc Format으로 변환
        private void buttonSaveAsPASCALVOCFormat_Click(object sender, EventArgs e)
        {
            int iListCount = listViewFiles.Items.Count;

            if (iListCount == 0)
            {
                MessageBox.Show("파일을 추가해 주세요");
                return;
            }
            else
            {
                // 경고 메시지
                DialogResult ret = MessageBox.Show("Filename will be overwritten. If you need, you should make a copy.", "Filename Overwritten Warning", MessageBoxButtons.YesNo);

                if (ret == DialogResult.No)
                    return;

                for (int iCount = 0; iCount < iListCount; iCount++)
                {
                    // ListView에서 이름 가져와서, 가져올 파일명 만들기
                    string strFileName = listViewFiles.Items[iCount].SubItems[1].Text;
                    string strDirectoryName = listViewFiles.Items[iCount].SubItems[2].Text;
                    string strFullFileName = strDirectoryName + "\\" + strFileName;

                    // txt 파일 불러서, 파일명 바꿔서 xml로 저장
                    string strFileNameWithoutExtension = Path.GetFileNameWithoutExtension(strFullFileName);
                    string strFullXmlName = strDirectoryName + "\\" + strFileNameWithoutExtension + ".xml";

                    // 저장할 이미지 파일 네임 (그냥 png로 정의)
                    string strImageName = Path.GetFileNameWithoutExtension(strFileName) + ".png";


                    // xml 정보 생성
                    XmlDocument xmlDoc = new XmlDocument();

                    // node 정의
                    XmlNode rootNode = xmlDoc.CreateElement("annotation");
                    XmlNode folderNode = xmlDoc.CreateElement("folder");
                    XmlNode filenameNode = xmlDoc.CreateElement("filename");
                    XmlNode pathNode = xmlDoc.CreateElement("path");
                    XmlNode sourceNode = xmlDoc.CreateElement("source");
                    XmlNode databaseNode = xmlDoc.CreateElement("database");
                    XmlNode sizeNode = xmlDoc.CreateElement("size");
                    XmlNode widthNode = xmlDoc.CreateElement("width");
                    XmlNode heightNode = xmlDoc.CreateElement("height");
                    XmlNode depthNode = xmlDoc.CreateElement("depth");
                    XmlNode segmentedNode = xmlDoc.CreateElement("segmented");

                    // 구조 정의
                    xmlDoc.AppendChild(rootNode);
                    rootNode.AppendChild(folderNode);
                    rootNode.AppendChild(filenameNode);
                    rootNode.AppendChild(pathNode);
                    rootNode.AppendChild(sourceNode);
                    sourceNode.AppendChild(databaseNode);
                    rootNode.AppendChild(sizeNode);
                    sizeNode.AppendChild(widthNode);
                    sizeNode.AppendChild(heightNode);
                    sizeNode.AppendChild(depthNode);
                    rootNode.AppendChild(segmentedNode);

                    // 내용 설정
                    // 일반 node 정보 가져오기
                    folderNode.InnerText = strDirectoryName;
                    filenameNode.InnerText = strImageName;
                    pathNode.InnerText = strDirectoryName + "\\" + strImageName;

                    databaseNode.InnerText = "Unknown";

                    widthNode.InnerText = "0";
                    heightNode.InnerText = "0";
                    depthNode.InnerText = "0";

                    segmentedNode.InnerText = 0.ToString();


                    // ICDAR 2013 Format 파일이 있는지 확인
                    if (File.Exists(strFullFileName))
                    {
                        // 파일 정보 가져오기
                        // 각 행에서 앞의 8개 숫자 및 마지막 9번째 문자 가져오기
                        using (StreamReader reader = new StreamReader(strFullFileName))
                        {                            
                            while (true)
                            {
                                string line = reader.ReadLine();

                                if (line == null)
                                    break;

                                string[] word = line.Split(',');

                                // object node 구조 정의
                                XmlNode objectNode = xmlDoc.CreateElement("object");
                                XmlNode objectNameNode = xmlDoc.CreateElement("name");
                                XmlNode objectPoseNode = xmlDoc.CreateElement("pose");
                                XmlNode objectTruncatedNode = xmlDoc.CreateElement("truncated");
                                XmlNode objectDifficultNode = xmlDoc.CreateElement("difficult");
                                XmlNode objectOccludedNode = xmlDoc.CreateElement("occluded");
                                XmlNode objectBndboxNode = xmlDoc.CreateElement("bndbox");
                                XmlNode objectBndboxXminNode = xmlDoc.CreateElement("xmin");
                                XmlNode objectBndboxYminNode = xmlDoc.CreateElement("ymin");
                                XmlNode objectBndboxXmaxNode = xmlDoc.CreateElement("xmax");
                                XmlNode objectBndboxYmaxNode = xmlDoc.CreateElement("ymax");

                                rootNode.AppendChild(objectNode);
                                objectNode.AppendChild(objectNameNode);
                                objectNode.AppendChild(objectPoseNode);
                                objectNode.AppendChild(objectTruncatedNode);
                                objectNode.AppendChild(objectDifficultNode);
                                objectNode.AppendChild(objectOccludedNode);
                                objectNode.AppendChild(objectBndboxNode);
                                objectBndboxNode.AppendChild(objectBndboxXminNode);
                                objectBndboxNode.AppendChild(objectBndboxYminNode);
                                objectBndboxNode.AppendChild(objectBndboxXmaxNode);
                                objectBndboxNode.AppendChild(objectBndboxYmaxNode);

                                // node node 내용 정의
                                objectNameNode.InnerText = word[8];

                                objectPoseNode.InnerText = "Unspecified";

                                objectTruncatedNode.InnerText = "0";

                                objectDifficultNode.InnerText = "0";

                                objectOccludedNode.InnerText = "0";

                                objectBndboxXminNode.InnerText = word[0];
                                objectBndboxYminNode.InnerText = word[1];
                                objectBndboxXmaxNode.InnerText = word[4];
                                objectBndboxYmaxNode.InnerText = word[5];
                            }
                        }
                        
                        // 파일명으로 저장
                        xmlDoc.Save(strFullXmlName);                        
                    }
                }
            }

            MessageBox.Show("변환이 완료되었습니다.");
        }
    }
}
