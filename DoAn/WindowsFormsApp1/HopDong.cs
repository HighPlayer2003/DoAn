using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace WindowsFormsApp1
{
    public partial class frmHopDong : Form
    {
        public frmHopDong()
        {
            InitializeComponent();
        }
        DataSet ds = new DataSet("dsKYTUCXA");
        SqlDataAdapter daSinhVien;
        SqlDataAdapter daNhanVien;
        SqlDataAdapter daPhong;
        SqlDataAdapter daHopDong;

        private void dgDSHopDong_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgDSHopDong.SelectedRows[0];
            txtMaHD.Text = dr.Cells["MAHOPDONG"].Value.ToString();
            dtpNgayBatDau.Text = dr.Cells["NGAYBATDAU"].Value.ToString();
            dtpNgayKetThuc.Text = dr.Cells["NGAYKETTHUC"].Value.ToString();

            dtpNgayLap.Text = dr.Cells["NGAYLAP"].Value.ToString();

            

            cboSoPhong.SelectedValue = dr.Cells["MAPHONG"].Value.ToString() ;
            cboTenSV.SelectedValue = dr.Cells["MASINHVIEN"].Value.ToString();
            cboTenNV.SelectedValue = dr.Cells["MANHANVIEN"].Value.ToString();
           
        }

        private void frmHopDong_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=KYTUCXA;Integrated Security=True";

            string sQuerySinhVien = @"SELECT * FROM SINHVIEN";
            daSinhVien = new SqlDataAdapter(sQuerySinhVien, conn);
            daSinhVien.Fill(ds, "tblSinhVien");

            cboTenSV.DataSource = ds.Tables["tblSinhVien"];
            cboTenSV.DisplayMember = "TENSINHVIEN";
            cboTenSV.ValueMember = "MASINHVIEN";

            string sQueryNhanVien = @"SELECT * FROM NHANVIEN";
            daNhanVien = new SqlDataAdapter(sQueryNhanVien, conn);
            daNhanVien.Fill(ds, "tblNhanVien");
            cboTenNV.DataSource = ds.Tables["tblNhanVien"];
            cboTenNV.DisplayMember = "TENNHANVIEN";
            cboTenNV.ValueMember = "MANHANVIEN";

            string sQueryPhong = @"SELECT * FROM PHONG";
            daPhong = new SqlDataAdapter(sQueryPhong, conn);
            daPhong.Fill(ds, "tblPhong");
            cboSoPhong.DataSource = ds.Tables["tblPhong"];

            cboSoPhong.DisplayMember = "SOPHONG";
            cboSoPhong.ValueMember = "MAPHONG";


            string sQueryHopDong = @"SELECT hd.*, sv.TENSINHVIEN, 
                     nv.TENNHANVIEN, p.SOPHONG
                    FROM HOPDONG hd, SINHVIEN sv, NHANVIEN nv, PHONG p
                    WHERE hd.MANHANVIEN = nv.MANHANVIEN AND hd.MASINHVIEN = 
                    sv.MASINHVIEN AND hd.MAPHONG = p.MAPHONG";

            daHopDong = new SqlDataAdapter(sQueryHopDong, conn);
            daHopDong.Fill(ds, "tblDSHopDong");
            dgDSHopDong.DataSource = ds.Tables["tblDSHopDong"];


            dgDSHopDong.Columns["MAHOPDONG"].HeaderText = "Mã hợp đồng";
            dgDSHopDong.Columns["TENNHANVIEN"].HeaderText = "Tên nhân viên";
            dgDSHopDong.Columns["TENSINHVIEN"].HeaderText = "Tên sinh viên";
            dgDSHopDong.Columns["SOPHONG"].HeaderText = "Số phòng";
            dgDSHopDong.Columns["NGAYLAP"].HeaderText = "Ngày lập";
            dgDSHopDong.Columns["NGAYBATDAU"].HeaderText = "Ngày bắt đầu";
            dgDSHopDong.Columns["NGAYKETTHUC"].HeaderText = "Ngày kết thúc";
            

            dgDSHopDong.Columns["MASINHVIEN"].Visible = false;
            dgDSHopDong.Columns["MANHANVIEN"].Visible = false;
            dgDSHopDong.Columns["MAPHONG"].Visible = false;


            //Them hop dong 
            string sThemHD = @"INSERT INTO HOPDONG (MAHOPDONG, MANHANVIEN, MASINHVIEN, MAPHONG, NGAYLAP, NGAYBATDAU, NGAYKETTHUC) 
                          VALUES (@MAHOPDONG, @MANHANVIEN, @MASINHVIEN, @MAPHONG, @NGAYLAP, @NGAYBATDAU, @NGAYKETTHUC)";

            SqlCommand cmThemHD = new SqlCommand(sThemHD, conn);
            cmThemHD.Parameters.Add("@MAHOPDONG", SqlDbType.VarChar, 12, "MAHOPDONG");
            cmThemHD.Parameters.Add("@MANHANVIEN", SqlDbType.VarChar, 13, "MANHANVIEN");
            cmThemHD.Parameters.Add("@MASINHVIEN", SqlDbType.VarChar, 9, "MASINHVIEN");
            cmThemHD.Parameters.Add("@MAPHONG", SqlDbType.VarChar, 12, "MAPHONG");
            cmThemHD.Parameters.Add("@NGAYLAP", SqlDbType.Date, 21, "NGAYLAP");

            cmThemHD.Parameters.Add("@NGAYBATDAU", SqlDbType.Date, 21, "NGAYBATDAU");
            cmThemHD.Parameters.Add("@NGAYKETTHUC", SqlDbType.Date, 21, "NGAYKETTHUC");

            daHopDong.InsertCommand = cmThemHD;

            // Sửa hợp đồng
            string sSuaHD = @"UPDATE HOPDONG 
                  SET MANHANVIEN = @MANHANVIEN, MASINHVIEN = @MASINHVIEN, MAPHONG = @MAPHONG, NGAYLAP = @NGAYLAP, NGAYBATDAU = @NGAYBATDAU, NGAYKETTHUC = @NGAYKETTHUC
                  WHERE MAHOPDONG = @MAHOPDONG";

            SqlCommand cmSuaHD = new SqlCommand(sSuaHD, conn);
            cmSuaHD.Parameters.Add("@MAHOPDONG", SqlDbType.VarChar, 12, "MAHOPDONG");
            cmSuaHD.Parameters.Add("@MANHANVIEN", SqlDbType.VarChar, 13, "MANHANVIEN");
            cmSuaHD.Parameters.Add("@MASINHVIEN", SqlDbType.VarChar, 9, "MASINHVIEN");
            cmSuaHD.Parameters.Add("@MAPHONG", SqlDbType.VarChar, 12, "MAPHONG");
            cmSuaHD.Parameters.Add("@NGAYLAP", SqlDbType.Date, 21, "NGAYLAP");

            cmSuaHD.Parameters.Add("@NGAYBATDAU", SqlDbType.Date, 21, "NGAYBATDAU");
            cmSuaHD.Parameters.Add("@NGAYKETTHUC", SqlDbType.Date, 21, "NGAYKETTHUC");

            daHopDong.UpdateCommand = cmSuaHD;

            // Xóa hợp đồng
            string sXoaHD = "DELETE FROM HOPDONG WHERE MAHOPDONG = @MAHOPDONG";

            SqlCommand cmXoaHD = new SqlCommand(sXoaHD, conn);
            cmXoaHD.Parameters.Add("@MAHOPDONG", SqlDbType.VarChar, 12, "MAHOPDONG");
            
            daHopDong.DeleteCommand = cmXoaHD;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;


        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables["tblDSHOPDONG"].NewRow();

            // Gán giá trị từ các controls vào các cột tương ứng của dòng mới
            row["MAHOPDONG"] = txtMaHD.Text;
            
            row["NGAYKETTHUC"] = dtpNgayKetThuc.Text;
            row["NGAYBATDAU"] = dtpNgayBatDau.Text;
            row["NGAYLAP"] = dtpNgayLap.Text;

            row["MASINHVIEN"] = cboTenSV.SelectedValue;
            row["TENSINHVIEN"] = cboTenSV.Text;

            row["MAPHONG"] = cboSoPhong.SelectedValue;
            row["SOPHONG"] = cboSoPhong.Text;



            row["MANHANVIEN"] = cboTenNV.SelectedValue;
            row["TENNHANVIEN"] = cboTenNV.Text;

            ds.Tables["tblDSHOPDONG"].Rows.Add(row);


            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                daHopDong.Update(ds, "tblDSHOPDONG");
                
                MessageBox.Show("Đã lưu!", "Thông báo");
                dgDSHopDong.Refresh();
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
           


                DataGridViewRow dr = dgDSHopDong.SelectedRows[0];
                dgDSHopDong.BeginEdit(true);

                dr.Cells["MAHOPDONG"].Value = txtMaHD.Text;
                dr.Cells["NGAYBATDAU"].Value = dtpNgayBatDau.Text;
                dr.Cells["NGAYKETTHUC"].Value = dtpNgayKetThuc.Text;
                dr.Cells["NGAYLAP"].Value = dtpNgayLap.Text;

                dr.Cells["MASINHVIEN"].Value = cboTenSV.SelectedValue;
                dr.Cells["TENSINHVIEN"].Value = cboTenSV.Text;
                dr.Cells["MANHANVIEN"].Value = cboTenNV.SelectedValue;
                dr.Cells["TENNHANVIEN"].Value = cboTenNV.Text;
                dr.Cells["MAPHONG"].Value = cboSoPhong.SelectedValue;
                dr.Cells["SOPHONG"].Value = cboSoPhong.Text;

            dgDSHopDong.EndEdit();
                MessageBox.Show("Đã sửa", "Thông báo");

                btnLuu.Enabled = true;
                btnHuy.Enabled = true;

            
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ds.Tables["tblDSHOPDONG"].RejectChanges();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgDSHopDong.SelectedRows[0];
            dgDSHopDong.Rows.Remove(dr);

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
            if (rdoMaHD.Checked)
            {
                string maHopDongCanTim = txtTimKiem.Text;

                DataView dv = new DataView(ds.Tables["tblDSHopDong"]);

                if (!string.IsNullOrEmpty(maHopDongCanTim))
                {
                    dv.RowFilter = $"MAHOPDONG = '{maHopDongCanTim}'";
                }

                dgDSHopDong.DataSource = dv.ToTable();

                if (string.IsNullOrEmpty(txtTimKiem.Text))
                {
                    dgDSHopDong.DataSource = ds.Tables["tblDSHopDong"];
                }
            }

            if (rdoTenSV.Checked)
            {
                string tenSVCamTim = txtTimKiem.Text;

                DataView dv = new DataView(ds.Tables["tblDSHopDong"]);

                if (!string.IsNullOrEmpty(tenSVCamTim))
                {
                    dv.RowFilter = $"TENSINHVIEN LIKE '%{tenSVCamTim}%'";
                }

                dgDSHopDong.DataSource = dv.ToTable();

                if (string.IsNullOrEmpty(txtTimKiem.Text))
                {
                    dgDSHopDong.DataSource = ds.Tables["tblDSHopDong"];
                }
            }
        }

    }
}
