using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace DoAn
{
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
        {
            InitializeComponent();
        }

        DataSet ds = new DataSet("dsKYTUCXA");

        SqlDataAdapter daPhong;
        SqlDataAdapter daNhanVien;
        SqlDataAdapter daHoaDon;




        private void HoaDon_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=KYTUCXA;Integrated Security=True";


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


            //(hd.TIENPHONG + hd.TIENDIEN + hd.TIENNUOC) AS 
            string sQueryHoaDon = @"
            SELECT 
                hd.MAHOADON, 
                hd.MANHANVIEN, 
                hd.MAPHONG, 
                hd.NGAYLAP, 
                hd.TIENPHONG, 
                hd.TIENDIEN, 
                hd.TIENNUOC, 
                hd.TONGTIEN, 
                p.SOPHONG, 
                nv.TENNHANVIEN
            FROM 
                HOADON hd
                INNER JOIN PHONG p ON hd.MAPHONG = p.MAPHONG
                INNER JOIN NHANVIEN nv ON hd.MANHANVIEN = nv.MANHANVIEN";



            daHoaDon = new SqlDataAdapter(sQueryHoaDon, conn);
            daHoaDon.Fill(ds, "tblDSHOADON");

            dgDSHoaDon.DataSource = ds.Tables["tblDSHOADON"];

            TinhTongTien();

            dgDSHoaDon.Columns["MAHOADON"].HeaderText = "Mã hóa đơn";
            dgDSHoaDon.Columns["TENNHANVIEN"].HeaderText = "Tên nhân viên";
            dgDSHoaDon.Columns["SOPHONG"].HeaderText = "Số phòng";
            dgDSHoaDon.Columns["NGAYLAP"].HeaderText = "Ngày lập";
            dgDSHoaDon.Columns["TIENPHONG"].HeaderText = "Tiền phòng";
            dgDSHoaDon.Columns["TIENDIEN"].HeaderText = "Tiền điện";
            dgDSHoaDon.Columns["TIENNUOC"].HeaderText = "Tiền nước";
            dgDSHoaDon.Columns["TONGTIEN"].HeaderText = "Tổng tiền";

            dgDSHoaDon.Columns["MANHANVIEN"].Visible = false;
            dgDSHoaDon.Columns["MAPHONG"].Visible = false;

            string sThemHD = @"INSERT INTO HOADON (MAHOADON, MANHANVIEN, MAPHONG, NGAYLAP, TIENPHONG, TIENDIEN, TIENNUOC, TONGTIEN) 
                        VALUES (@MAHOADON, @MANHANVIEN, @MAPHONG, @NGAYLAP, @TIENPHONG, @TIENDIEN, @TIENNUOC, @TONGTIEN)";

            SqlCommand cmThemHD = new SqlCommand(sThemHD, conn);

            cmThemHD.Parameters.Add("@MAHOADON", SqlDbType.VarChar, 10, "MAHOADON");
            cmThemHD.Parameters.Add("@MANHANVIEN", SqlDbType.VarChar, 13, "MANHANVIEN");
            cmThemHD.Parameters.Add("@MAPHONG", SqlDbType.VarChar, 12, "MAPHONG");
            cmThemHD.Parameters.Add("@NGAYLAP", SqlDbType.Date, 20, "NGAYLAP");
            cmThemHD.Parameters.Add("@TIENPHONG", SqlDbType.Money, 21, "TIENPHONG");
            cmThemHD.Parameters.Add("@TIENDIEN", SqlDbType.Money, 21, "TIENDIEN");
            cmThemHD.Parameters.Add("@TIENNUOC", SqlDbType.Money, 21, "TIENNUOC");
            cmThemHD.Parameters.Add("@TONGTIEN", SqlDbType.Money, 21, "TONGTIEN");

            daHoaDon.InsertCommand = cmThemHD;

            string sSuaHD = @"UPDATE HOADON 
                     SET MANHANVIEN = @MANHANVIEN,
                         MAPHONG = @MAPHONG,
                         NGAYLAP = @NGAYLAP,
                         TIENPHONG = @TIENPHONG,
                         TIENDIEN = @TIENDIEN,
                         TIENNUOC = @TIENNUOC,
                         TONGTIEN = @TONGTIEN
                         
                     WHERE MAHOADON = @MAHOADON";

            SqlCommand cmSuaHD = new SqlCommand(sSuaHD, conn);
            cmSuaHD.Parameters.Add("@MAHOADON", SqlDbType.VarChar, 10, "MAHOADON");
            cmSuaHD.Parameters.Add("@MANHANVIEN", SqlDbType.VarChar, 13, "MANHANVIEN");
            cmSuaHD.Parameters.Add("@MAPHONG", SqlDbType.VarChar, 12, "MAPHONG");
            cmSuaHD.Parameters.Add("@NGAYLAP", SqlDbType.Date, 20, "NGAYLAP");
            cmSuaHD.Parameters.Add("@TIENPHONG", SqlDbType.Money, 21, "TIENPHONG");
            cmSuaHD.Parameters.Add("@TIENDIEN", SqlDbType.Money, 21, "TIENDIEN");
            cmSuaHD.Parameters.Add("@TIENNUOC", SqlDbType.Money, 21, "TIENNUOC");
            cmSuaHD.Parameters.Add("@TONGTIEN", SqlDbType.Money, 21, "TONGTIEN");

            daHoaDon.UpdateCommand = cmSuaHD;

            string sXoaHD = @"DELETE FROM HOADON WHERE MAHOADON = @MAHOADON";
            SqlCommand cmXoaHD = new SqlCommand(sXoaHD, conn);
            cmXoaHD.Parameters.Add("@MAHOADON", SqlDbType.VarChar, 10, "MAHOADON");

            daHoaDon.DeleteCommand = cmXoaHD;



            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }


        private void dgDSHoaDon_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgDSHoaDon.SelectedRows[0];
            txtMaHD.Text = dr.Cells["MAHOADON"].Value.ToString();
            cboTenNV.SelectedValue = dr.Cells["MANHANVIEN"].Value.ToString();
            cboSoPhong.SelectedValue = dr.Cells["MAPHONG"].Value.ToString();
            dtpNgayLap.Text = dr.Cells["NGAYLAP"].Value.ToString();
            txtTienPhong.Text = dr.Cells["TIENPHONG"].Value.ToString();
            txtTienDien.Text = dr.Cells["TIENDIEN"].Value.ToString();
            txtTienNuoc.Text = dr.Cells["TIENNUOC"].Value.ToString();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            DataRow row = ds.Tables["tblDSHOADON"].NewRow();

            // Gán giá trị từ các controls vào các cột tương ứng của dòng mới
            row["MAHOADON"] = txtMaHD.Text;

            row["NGAYLAP"] = dtpNgayLap.Text;
            row["TIENPHONG"] = txtTienPhong.Text;
            row["TIENDIEN"] = txtTienDien.Text;
            row["TIENNUOC"] = txtTienNuoc.Text;

            row["MANHANVIEN"] = cboTenNV.SelectedValue;
            row["TENNHANVIEN"] = cboTenNV.Text;

            row["MAPHONG"] = cboSoPhong.SelectedValue;
            row["SOPHONG"] = cboSoPhong.Text;

            // Tính toán giá trị TONGTIEN và gán vào cột tương ứng
            row["TONGTIEN"] = TinhTongTien(txtTienPhong.Text, txtTienDien.Text, txtTienNuoc.Text);

            // Thêm dòng mới vào DataTable
            ds.Tables["tblDSHOADON"].Rows.Add(row);

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgDSHoaDon.SelectedRows[0];
            dgDSHoaDon.BeginEdit(true);

            dr.Cells["MAHOADON"].Value = txtMaHD.Text;

            dr.Cells["NGAYLAP"].Value = dtpNgayLap.Text;
            dr.Cells["TIENPHONG"].Value = txtTienPhong.Text;
            dr.Cells["TIENDIEN"].Value = txtTienDien.Text;
            dr.Cells["TIENNUOC"].Value = txtTienNuoc.Text;

            dr.Cells["MANHANVIEN"].Value = cboTenNV.SelectedValue;
            dr.Cells["TENNHANVIEN"].Value = cboTenNV.Text;
            dr.Cells["MAPHONG"].Value = cboSoPhong.SelectedValue;
            dr.Cells["SOPHONG"].Value = cboSoPhong.Text;

            // Tính toán giá trị TONGTIEN và gán vào cột tương ứng
            dr.Cells["TONGTIEN"].Value = TinhTongTien(txtTienPhong.Text, txtTienDien.Text, txtTienNuoc.Text);

            dgDSHoaDon.EndEdit();
            MessageBox.Show("Đã sửa", "Thông báo");

            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }










        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {

                daHoaDon.Update(ds, "tblDSHOADON");

                MessageBox.Show("Đã lưu!", "Thông báo");
                dgDSHoaDon.Refresh();
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
                return;
            }
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;


        }

        public void TinhTongTien()
        {
            foreach (DataRow row in ds.Tables["tblDSHoaDon"].Rows)
            {
                decimal tienDien = Convert.ToDecimal(row["TIENDIEN"]);
                decimal tienNuoc = Convert.ToDecimal(row["TIENNUOC"]);
                decimal tienPhong = Convert.ToDecimal(row["TIENPHONG"]);

                decimal tongTien = tienDien + tienNuoc + tienPhong;

                // Lưu giá trị Tổng tiền vào cột mới
                row["TONGTIEN"] = tongTien;
            }
        }



        private decimal TinhTongTien(string tienPhong, string tienDien, string tienNuoc)
        {
            decimal tiPhong, tiDien, tiNuoc;
            decimal.TryParse(tienPhong, out tiPhong);
            decimal.TryParse(tienDien, out tiDien);
            decimal.TryParse(tienNuoc, out tiNuoc);

            return tiPhong + tiDien + tiNuoc;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ds.Tables["tblDSHOADON"].RejectChanges();


        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgDSHoaDon.SelectedRows[0];
            dgDSHoaDon.Rows.Remove(dr);



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
                string maHoaDonCanTim = txtTimKiem.Text;

                DataView dv = new DataView(ds.Tables["tblDSHOADON"]);

                if (!string.IsNullOrEmpty(maHoaDonCanTim))
                {
                    dv.RowFilter = $"MAHOADON = '{maHoaDonCanTim}'";
                }

                dgDSHoaDon.DataSource = dv.ToTable();

                if (string.IsNullOrEmpty(txtTimKiem.Text))
                {
                    dgDSHoaDon.DataSource = ds.Tables["tblDSHOADON"];
                }
            }

            if (rdoSoPhong.Checked)
            {
                string soPhongCanTim = txtTimKiem.Text;

                DataView dv = new DataView(ds.Tables["tblDSHOADON"]);

                if (!string.IsNullOrEmpty(soPhongCanTim))
                {
                    dv.RowFilter = $"SOPHONG LIKE '%{soPhongCanTim}%'";
                }

                dgDSHoaDon.DataSource = dv.ToTable();

                if (string.IsNullOrEmpty(txtTimKiem.Text))
                {
                    dgDSHoaDon.DataSource = ds.Tables["tblDSHOADON"];
                }
            }
        }
    }
}