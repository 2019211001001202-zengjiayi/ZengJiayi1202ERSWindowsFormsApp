using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZengJiayi1202ERSWindowsFormsApp
{
    public partial class EmployeeRecordsForm : Form
    {
        private TreeNode tvRootNode;

        public EmployeeRecordsForm()
        {
            InitializeComponent();
            PopulateTreeViem();
            initalizeListView();
        }

        private void EmployeeRecordsForm_Load(object sender, EventArgs e)
        {

        }
        private void PopulateTreeViem()
        {
            statusBarPanel1.Tag = "Refreshing Employee Code. Please Wait...";
            this.Cursor= Cursors.WaitCursor;
            treeView1.Nodes.Clear();
            tvRootNode = new TreeNode("Emplyoee Records");
            this.Cursor = Cursors.Default;
            treeView1.Nodes.Add(tvRootNode);

            TreeNodeCollection nodeCollection=tvRootNode.Nodes;
            XmlTextReader reader = new XmlTextReader("C:\\Users\\Administrator\\source\\repos\\ZengJiayi1202ERSWindowsFormsApp\\ZengJiayi1202ERSWindowsFormsApp\\EmpRec.xml");
            reader.MoveToContent();
            try
            {
                while (reader.Read())
                {
                    if(reader.HasAttributes && reader.NodeType == XmlNodeType.Element)
                    {
                        reader.MoveToElement();//<EmpRecordsData>
                        reader.MoveToElement();//<Ecode>

                        reader.MoveToAttribute("Id");//Id="E001"
                        String strVal=reader.Value;//E001
                        reader.Read();
                        reader.Read();
                        if (reader.Name == "Dept")
                        {
                            reader.Read();

                        }
                        TreeNode EcodeNode=new TreeNode(strVal);
                        nodeCollection.Add(EcodeNode);


                    }
                }
                statusBarPanel1.Tag = "Click on an emplyoee code to see their record.";
            }
            catch(XmlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected void initalizeListView()
        {
            listView1.Clear();
            listView1.Columns.Add("Employee Name",255,HorizontalAlignment.Left );
            listView1.Columns.Add("Date of Join",70,HorizontalAlignment.Right );
            listView1.Columns.Add("Gread",105,HorizontalAlignment.Left );
            listView1.Columns.Add("Salary",105,HorizontalAlignment.Left );

        }
        protected void PopulateListView(TreeNode crrNode)
        {
            initalizeListView();
            XmlTextReader listRead= new XmlTextReader("C:\\Users\\Administrator\\source\\repos\\ZengJiayi1202ERSWindowsFormsApp\\ZengJiayi1202ERSWindowsFormsApp\\EmpRec.xml");
            listRead.MoveToElement();
            while (listRead.Read())
            {
                String strNodeName; 
                String strNodePath;
                String name;
                String gread;
                String doj;
                String sal;
                String[] strItemsArr = new String[4];
                listRead.MoveToFirstAttribute();
                strNodeName = listRead.Value;
                strNodePath = crrNode.FullPath.Remove(0,17);
                if (strNodePath == strNodePath)
                {
                    ListViewItem lvi;

                    listRead.MoveToFirstAttribute();
                    name= listRead.Value;
                    lvi = listView1.Items.Add(listRead.Value);

                    listRead.Read();
                    listRead.Read();

                    listRead.MoveToFirstAttribute();
                    doj = listRead.Value;
                    lvi.SubItems.Add(doj);

                    listRead.MoveToFirstAttribute();
                    gread = listRead.Value;
                    lvi.SubItems.Add(gread);

                    listRead.MoveToFirstAttribute();
                    sal = listRead.Value;
                    lvi.SubItems.Add(sal);

                    listRead.MoveToFirstAttribute();
                    listRead.MoveToElement();
                    listRead.ReadString();


                }
            }
        }
        private void treeView1_AfterSelect(object sender,TreeViewEventArgs e)
        {
            TreeNode currNode = e.Node;
            if (tvRootNode == currNode)
            {
                
                statusBarPanel1.Text = "Double Click the Employeee Records";
                return;

            }
            else
            {
                statusBarPanel1.Text = "Click an Employee code to view individual record";

            }
            PopulateListView(currNode);

        }
    }
}
