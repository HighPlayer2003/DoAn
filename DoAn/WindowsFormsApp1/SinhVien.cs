using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmSinhVien : Form
    {
        public frmSinhVien()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet("dsKYTUCXA");
        SqlDataAdapter daKhoa;
        SqlDataAdapter daLop;
        SqlDataAdapter daSinhVien;


        private void Form4_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=KYTUCXA;Integrated Security=True";


            

            string sQueryKhoa = @"SELECT * FROM KHOA ";
            daKhoa = new SqlDataAdapter(sQueryKhoa, conn);
            daKhoa.Fill(ds, "tblKhoa");
            cboTenKhoa.DataSource = ds.Tables["tblKhoa"];
            cboTenKhoa.DisplayMember = "TENKHOA";
            cboTenKhoa.ValueMember = "MAKHOA";

            string sQueryLop  = @"SELECT * FROM LOP";
            daLop = new SqlDataAdapter(sQueryLop, conn);
            daLop.Fill(ds, "tblLop");
            cboTenLop.DataSource = ds.Tables["tblLop"];
            cboTenLop.DisplayMember = "TENLOP";
            cboTenLop.ValueMember = "MALOP";


            string sQuerySinhVien = @"SELECT sv.*, l.TENLOP, k.TENKHOA
                    FROM SINHVIEN sv, LOP l, KHOA k
                    WHERE sv.MALOP = l.MALOP AND sv.MAKHOA = k.MAKHOA ";


            daSinhVien = new SqlDataAdapter(sQuerySinhVien, conn);
            daSinhVien.Fill(ds, "tblDSSinhVien");
            dgDSSinhVien.DataSource = ds.Tables["tblDSSinhVien"];

            dgDSSinhVien.Columns["MASINHVIEN"].HeaderText = "Mã sinh viên";
            dgDSSinhVien.Columns["TENSINHVIEN"].HeaderText = "Tên nhân viên";
            dgDSSinhVien.Columns["NGAYSINH"].HeaderText = "Ngày sinh";
            dgDSSinhVien.Columns["GIOITINH"].HeaderText = "Giới tính";
            dgDSSinhVien.Columns["NOISINH"].HeaderText = "Nơi sinh";
            dgDSSinhVien.Columns["DIACHI"].HeaderText = "Địa chỉ";
            dgDSSinhVien.Columns["TENLOP"].HeaderText = "Lớp";
            dgDSSinhVien.Columns["TENKHOA"].HeaderText = "Khoa";
            dgDSSinhVien.Columns["CCCD"].HeaderText = "CCCD";
            dgDSSinhVien.Columns["SDT"].HeaderText = "Số điện thoại";
            dgDSSinhVien.Columns["EMAIL"].HeaderText = "Email";

            dgDSSinhVien.Columns["MALOP"].Visible = false;
            dgDSSinhVien.Columns["MAKHOA"].Visible = false;



            // Sửa truy vấn INSERT để thêm vào bảng SinhVien
            string sThemSV = @"INSERT INTO SINHVIEN (MASINHVIEN, TENSINHVIEN, NGAYSINH, GIOITINH, NOISINH, DIACHI, MALOP, MAKHOA, CCCD, SDT, EMAIL) 
                    VALUES (@MASINHVIEN, @TENSINHVIEN, @NGAYSINH, @GIOITINH, @NOISINH, @DIACHI, @MALOP, @MAKHOA,@CCCD, @SDT, @EMAIL)";

            SqlCommand cmThemSV = new SqlCommand(sThemSV, conn);
            cmThemSV.Parameters.Add("@MASINHVIEN", SqlDbType.VarChar, 9, "MASINHVIEN"); 
            cmThemSV.Parameters.Add("@TENSINHVIEN", SqlDbType.NVarChar, 50, "TENSINHVIEN"); 
            cmThemSV.Parameters.Add("@NGAYSINH", SqlDbType.Date, 20, "NGAYSINH"); 
            cmThemSV.Parameters.Add("@GIOITINH", SqlDbType.NVarChar, 3, "GIOITINH"); 
            cmThemSV.Parameters.Add("@NOISINH", SqlDbType.NVarChar, 30, "NOISINH"); 

            cmThemSV.Parameters.Add("@DIACHI", SqlDbType.NVarChar, 50, "DIACHI"); 
            cmThemSV.Parameters.Add("@MALOP", SqlDbType.VarChar,10 , "MALOP");
            cmThemSV.Parameters.Add("@MAKHOA", SqlDbType.VarChar, 13, "MAKHOA");
            cmThemSV.Parameters.Add("@CCCD", SqlDbType.Char, 12, "CCCD"); 
            cmThemSV.Parameters.Add("@SDT", SqlDbType.Char, 10, "SDT"); 
            cmThemSV.Parameters.Add("@EMAIL", SqlDbType.VarChar, 40, "EMAIL"); 

            daSinhVien.InsertCommand = cmThemSV; // Đặt InsertCommand của DataAdapter là câu lệnh cmThemSV

            // Sửa thông tin của Sinh Viên
            string sSuaSV = @"UPDATE SINHVIEN SET TENSINHVIEN = @TENSINHVIEN, NGAYSINH = @NGAYSINH, GIOITINH = @GIOITINH, 
                    NOISINH = @NOISINH, DIACHI = @DIACHI, MALOP = @MALOP, MAKHOA = @MAKHOA, CCCD = @CCCD, SDT = @SDT, EMAIL = @EMAIL 
                    WHERE MASINHVIEN = @MASINHVIEN"; // Sửa thông tin của Sinh Viên dựa trên MASINHVIEN

            SqlCommand cmSuaSV = new SqlCommand(sSuaSV, conn);
            cmSuaSV.Parameters.Add("@TENSINHVIEN", SqlDbType.NVarChar, 50, "TENSINHVIEN"); 
            cmSuaSV.Parameters.Add("@NGAYSINH", SqlDbType.Date, 20, "NGAYSINH"); 
            cmSuaSV.Parameters.Add("@GIOITINH", SqlDbType.NVarChar, 3, "GIOITINH"); 
            cmSuaSV.Parameters.Add("@NOISINH", SqlDbType.NVarChar, 30, "NOISINH"); 
            cmSuaSV.Parameters.Add("@DIACHI", SqlDbType.NVarChar, 50, "DIACHI"); 
            cmSuaSV.Parameters.Add("@MALOP", SqlDbType.VarChar, 10, "MALOP");
            cmSuaSV.Parameters.Add("@MAKHOA", SqlDbType.VarChar, 13, "MAKHOA");

            cmSuaSV.Parameters.Add("@CCCD", SqlDbType.Char, 12, "CCCD"); 
            cmSuaSV.Parameters.Add("@SDT", SqlDbType.Char, 10, "SDT"); 
            cmSuaSV.Parameters.Add("@EMAIL", SqlDbType.VarChar, 40, "EMAIL"); 
            cmSuaSV.Parameters.Add("@MASINHVIEN", SqlDbType.VarChar, 9, "MASINHVIEN"); 

            daSinhVien.UpdateCommand = cmSuaSV; // Đặt UpdateCommand của DataAdapter là câu lệnh cmSuaSV

            // Xóa thông tin của Sinh Viên
            string sXoaSV = @"DELETE FROM SINHVIEN WHERE MASINHVIEN = @MASINHVIEN"; // Xóa thông tin của Sinh Viên dựa trên MASINHVIEN

            SqlCommand cmXoaSV = new SqlCommand(sXoaSV, conn);
            cmXoaSV.Parameters.Add("@MASINHVIEN", SqlDbType.VarChar, 9, "MASINHVIEN"); // Đặt kiểu dữ liệu và kích thước cho MASINHVIEN

            daSinhVien.DeleteCommand = cmXoaSV; // Đặt DeleteCommand của DataAdapter là câu lệnh cmXoaSV

            btnLuu.Enabled = false;
            btnHuy.Enabled = false;

        }

        private void dgDSSinhVien_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgDSSinhVien.SelectedRows[0];
            txtMaSV.Text = dr.Cells["MASINHVIEN"].Value.ToString();
            txtTenSV.Text = dr.Cells["TENSINHVIEN"].Value.ToString();
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
            cboTenLop.SelectedValue = dr.Cells["MALOP"].Value.ToString();
            txtCCCD.Text = dr.Cells["CCCD"].Value.ToString();
            txtSDT.Text = dr.Cells["SDT"].Value.ToString();
            txtEmail.Text = dr.Cells["EMAIL"].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables["tblDSSINHVIEN"].NewRow();

            // Gán giá trị từ các controls vào các cột tương ứng của dòng mới
            row["MASINHVIEN"] = txtMaSV.Text;
            row["TENSINHVIEN"] = txtTenSV.Text;
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
            row["MALOP"] = cboTenLop.SelectedValue;
            row["TENLOP"] = cboTenLop.Text;

            row["MAKHOA"] = cboTenKhoa.SelectedValue;
            row["TENKHOA"] = cboTenKhoa.Text;


            row["CCCD"] = txtCCCD.Text;
            row["SDT"] = txtSDT.Text;
            row["EMAIL"] = txtEmail.Text;

            // Thêm dòng mới vào DataTable
            ds.Tables["tblDSSINHVIEN"].Rows.Add(row);

            

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                daSinhVien.Update(ds, "tblDSSINHVIEN");
                
                MessageBox.Show("Đã lưu!", "Thông báo");
                dgDSSinhVien.Refresh();
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


            DataGridViewRow dr = dgDSSinhVien.SelectedRows[0];
            dgDSSinhVien.BeginEdit(true);

            dr.Cells["MASINHVIEN"].Value = txtMaSV.Text;
            dr.Cells["TENSINHVIEN"].Value = txtTenSV.Text;
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
            dr.Cells["MALOP"].Value = cboTenLop.SelectedValue;
            dr.Cells["MAKHOA"].Value = cboTenKhoa.SelectedValue;
            dr.Cells["CCCD"].Value = txtCCCD.Text;
            dr.Cells["SDT"].Value = txtSDT.Text;
            dr.Cells["EMAIL"].Value = txtEmail.Text;

            dgDSSinhVien.EndEdit();
            MessageBox.Show("Đã sửa", "Thông báo");

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ds.Tables["tblDSSINHVIEN"].RejectChanges();

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            DataGridViewRow dr = dgDSSinhVien.SelectedRows[0];
            dgDSSinhVien.Rows.Remove(dr);

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;


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
            if (rdoMaSV.Checked)
            {
                string maSinhVienCanTim = txtTimKiem.Text;

                // Sử dụng DataView để lọc dữ liệu từ DataSet
                DataView dv = new DataView(ds.Tables["tblDSSinhVien"]);
                dv.RowFilter = $"MASINHVIEN = '{maSinhVienCanTim}'";

                if (dv.Count > 0)
                {
                    dgDSSinhVien.DataSource = dv.ToTable();
                }

                if (txtTimKiem.Text == "")
                {
                    dgDSSinhVien.DataSource = ds.Tables["tblDSSinhVien"];
                }
            }

            if (rdoTenSV.Checked)
            {
                string tenSinhVienCanTim = txtTimKiem.Text;

                DataView dv = new DataView(ds.Tables["tblDSSinhVien"]);

                if (!string.IsNullOrEmpty(tenSinhVienCanTim))
                {
                    dv.RowFilter = $"TENSINHVIEN LIKE '%{tenSinhVienCanTim}%'";
                }

                dgDSSinhVien.DataSource = dv.ToTable();

                if (txtTimKiem.Text == "")
                {
                    dgDSSinhVien.DataSource = ds.Tables["tblDSSinhVien"];
                }
            }
        }

    }
}
