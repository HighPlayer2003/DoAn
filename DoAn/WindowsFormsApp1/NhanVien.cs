using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using DoAn;

namespace WindowsFormsApp1
{
    public partial class frmNhanVien : Form
    {


        public frmNhanVien()
        {
            InitializeComponent();
            
        }



        DataSet ds = new DataSet("dsKYTUCXA");
        SqlDataAdapter daNhanVien;


        private void Form2_Load(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=KYTUCXA;Integrated Security=True";
            conn.Open();

            string sQueryNhanVien = @"SELECT * FROM NHANVIEN";
            daNhanVien = new SqlDataAdapter(sQueryNhanVien, conn);

            daNhanVien.Fill(ds, "tblDSNHANVIEN");
            dgDSNhanVien.DataSource = ds.Tables["tblDSNHANVIEN"];
            dgDSNhanVien.Columns["MANHANVIEN"].HeaderText = "Mã nhân viên";
            dgDSNhanVien.Columns["TENNHANVIEN"].HeaderText = "Tên nhân viên";
            dgDSNhanVien.Columns["NGAYSINH"].HeaderText = "Ngày sinh";
            dgDSNhanVien.Columns["GIOITINH"].HeaderText = "Giới tính";
            dgDSNhanVien.Columns["NOISINH"].HeaderText = "Nơi sinh";
            dgDSNhanVien.Columns["DIACHI"].HeaderText = "Địa chỉ";
            dgDSNhanVien.Columns["CCCD"].HeaderText = "CCCD";
            dgDSNhanVien.Columns["SDT"].HeaderText = "Số điện thoại";
            dgDSNhanVien.Columns["EMAIL"].HeaderText = "Email";

            //Them nhan vien
            string sThemNV = @"INSERT INTO NHANVIEN VALUES(@MANHANVIEN, @TENNHANVIEN, @NGAYSINH, @GIOITINH, @NOISINH, @DIACHI, @CCCD, @SDT, @EMAIL)";

            SqlCommand cmThemNV = new SqlCommand(sThemNV, conn);
            cmThemNV.Parameters.Add("@MANHANVIEN", SqlDbType.VarChar, 13, "MANHANVIEN");
            cmThemNV.Parameters.Add("@TENNHANVIEN", SqlDbType.NVarChar, 50, "TENNHANVIEN");
            cmThemNV.Parameters.Add("@NGAYSINH", SqlDbType.Date, 10, "NGAYSINH");
            cmThemNV.Parameters.Add("@GIOITINH", SqlDbType.NVarChar, 3, "GIOITINH");
            cmThemNV.Parameters.Add("@NOISINH", SqlDbType.NVarChar, 30, "NOISINH");

            cmThemNV.Parameters.Add("@DIACHI", SqlDbType.NVarChar, 50, "DIACHI");
            cmThemNV.Parameters.Add("@CCCD", SqlDbType.Char, 12, "CCCD");
            cmThemNV.Parameters.Add("@SDT", SqlDbType.Char, 10, "SDT");
            cmThemNV.Parameters.Add("@EMAIL", SqlDbType.VarChar, 40, "EMAIL");

            daNhanVien.InsertCommand = cmThemNV;

            //Sua nhan vien
            string sSuaNV = @"UPDATE NHANVIEN SET TENNHANVIEN = @TENNHANVIEN, NGAYSINH = @NGAYSINH, GIOITINH = @GIOITINH, NOISINH = @NOISINH, DIACHI = @DIACHI, CCCD = @CCCD, SDT = @SDT, EMAIL = @EMAIL 
                WHERE MANHANVIEN = @MANHANVIEN";

            SqlCommand cmSuaNV = new SqlCommand(sSuaNV, conn);
            cmSuaNV.Parameters.Add("@MANHANVIEN", SqlDbType.VarChar, 13, "MANHANVIEN");
            cmSuaNV.Parameters.Add("@TENNHANVIEN", SqlDbType.NVarChar, 50, "TENNHANVIEN");
            cmSuaNV.Parameters.Add("@NGAYSINH", SqlDbType.Date, 10, "NGAYSINH");
            cmSuaNV.Parameters.Add("@GIOITINH", SqlDbType.NVarChar, 3, "GIOITINH");
            cmSuaNV.Parameters.Add("@NOISINH", SqlDbType.NVarChar, 30, "NOISINH");

            cmSuaNV.Parameters.Add("@DIACHI", SqlDbType.NVarChar, 50, "DIACHI");
            cmSuaNV.Parameters.Add("@CCCD", SqlDbType.Char, 12, "CCCD");
            cmSuaNV.Parameters.Add("@SDT", SqlDbType.Char, 10, "SDT");
            cmSuaNV.Parameters.Add("@EMAIL", SqlDbType.VarChar, 40, "EMAIL");

            daNhanVien.UpdateCommand = cmSuaNV;

            //Xóa nhan vien
            string sXoaNV = "DELETE FROM NHANVIEN WHERE MANHANVIEN = @MANHANVIEN";

            SqlCommand cmXoaNV = new SqlCommand(sXoaNV, conn);
            cmXoaNV.Parameters.Add("@MANHANVIEN", SqlDbType.VarChar, 13, "MANHANVIEN");

            daNhanVien.DeleteCommand = cmXoaNV;




            btnLuu.Enabled = false;
            btnHuy.Enabled = false;



            

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables["tblDSNHANVIEN"].NewRow();
            row["MANHANVIEN"] = txtManhanvien.Text;
            row["TENNHANVIEN"] = txtTennhanvien.Text;
            row["NGAYSINH"] = dtpNgaySinh.Text;

            if (rdoNam.Checked == true)
            {
                row["GIOITINH"] = "Nam";
            }
            else
            {
                row["GIOITINH"] = "Nữ";
            }
            row["NOISINH"] = txtNoisinh.Text;
            row["DIACHI"] = txtDiachi.Text;
            row["CCCD"] = txtCCCD.Text;
            row["SDT"] = txtSDT.Text;
            row["EMAIL"] = txtEmail.Text;

            ds.Tables["tblDSNHANVIEN"].Rows.Add(row);

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

        }



        private void dgDSNhanVien_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgDSNhanVien.SelectedRows[0];
            txtManhanvien.Text = dr.Cells["MANHANVIEN"].Value.ToString();
            txtTennhanvien.Text = dr.Cells["TENNHANVIEN"].Value.ToString();
            dtpNgaySinh.Text = dr.Cells["NGAYSINH"].Value.ToString();

            if (dr.Cells["GIOITINH"].Value.ToString() == "Nam")
            {
                rdoNam.Checked = true;
            }
            else
            {
                rdoNu.Checked = true;
            }

            txtNoisinh.Text = dr.Cells["NOISINH"].Value.ToString();
            txtDiachi.Text = dr.Cells["DIACHI"].Value.ToString();
            txtCCCD.Text = dr.Cells["CCCD"].Value.ToString();
            txtSDT.Text = dr.Cells["SDT"].Value.ToString();
            txtEmail.Text = dr.Cells["EMAIL"].Value.ToString();


        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                daNhanVien.Update(ds, "tblDSNHANVIEN");
                MessageBox.Show("Đã lưu!", "Thông báo");
                dgDSNhanVien.Refresh();
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
                return;
            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;


        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgDSNhanVien.SelectedRows[0];
            dgDSNhanVien.BeginEdit(true);

            dr.Cells["MANHANVIEN"].Value = txtManhanvien.Text;
            dr.Cells["TENNHANVIEN"].Value = txtTennhanvien.Text;
            dr.Cells["NGAYSINH"].Value = dtpNgaySinh.Text;

            if (rdoNam.Checked == true)
            {
                dr.Cells["GIOITINH"].Value = "Nam";
            }
            else
            {
                dr.Cells["GIOITINH"].Value = "Nữ";
            }
            dr.Cells["NOISINH"].Value = txtNoisinh.Text;
            dr.Cells["DIACHI"].Value = txtDiachi.Text;
            dr.Cells["CCCD"].Value = txtCCCD.Text;
            dr.Cells["SDT"].Value = txtSDT.Text;
            dr.Cells["EMAIL"].Value = txtEmail.Text;

            dgDSNhanVien.EndEdit();
            MessageBox.Show("Đã sửa", "Thông báo");

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgDSNhanVien.SelectedRows[0];
            dgDSNhanVien.Rows.Remove(dr);

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ds.Tables["tblDSNHANVIEN"].RejectChanges();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Bạn có muốn đóng chương trình không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK)
                Close();
        }



        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (rdoMaNV.Checked)
            {
                string maNhanVienCanTim = txtTimKiem.Text;

                // Sử dụng DataView để lọc dữ liệu từ DataSet
                DataView dv = new DataView(ds.Tables["tblDSNHANVIEN"]);
                dv.RowFilter = $"MANHANVIEN = '{maNhanVienCanTim}'";

                if (dv.Count > 0)
                {
                    dgDSNhanVien.DataSource = dv.ToTable();
                }

                if (txtTimKiem.Text == "")
                {
                    dgDSNhanVien.DataSource = ds.Tables["tblDSNHANVIEN"];
                }
            }

            if (rdoTenNV.Checked)
            {
                string tenNhanVienCanTim = txtTimKiem.Text;

                DataView dv = new DataView(ds.Tables["tblDSNHANVIEN"]);

                if (!string.IsNullOrEmpty(tenNhanVienCanTim))
                {
                    dv.RowFilter = $"TENNHANVIEN LIKE '%{tenNhanVienCanTim}%'";
                }

                dgDSNhanVien.DataSource = dv.ToTable();



                if (txtTimKiem.Text == "")
                {
                    dgDSNhanVien.DataSource = ds.Tables["tblDSNHANVIEN"];
                }

            }
        }

        
    }
}
