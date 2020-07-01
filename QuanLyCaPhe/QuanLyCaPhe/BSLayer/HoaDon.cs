﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCaPhe.DBLayer;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyCaPhe.BSLayer
{
    public class HoaDon
    {

        string err;
        public HoaDon()
        {

        }

        public DataSet LayHoaDon()
        {
            //return DBMain.getInstance().ExecuteQueryDataSet("select * from HoaDon", CommandType.Text);
            return DBMain.getInstance().ExecuteQueryDataSet("Read_HoaDon", CommandType.StoredProcedure);
        }

        public int LayIDHoaDonTheoBan(int idBan)
        {
            //DataTable dt = DBMain.getInstance().ExecuteQueryDataSet($"select * from HoaDon where IDBanAn = '{idBan}' and HoaDon.TinhTrang = 0", CommandType.Text).Tables[0];
            DataTable dt = DBMain.getInstance().ExecuteQueryDataSet("Read_HoaDon_IDBanAn", CommandType.StoredProcedure, new SqlParameter("@idBanAn", idBan)).Tables[0];
            int id = -1;    //ID Hóa đơn mặc định không tìm thấy
            //Kiểm tra xem dt có dữ liệu hay không ?
            if (dt.Rows.Count > 0)
                id = (int)dt.Rows[0]["IDHoaDon"];            
            return id;
        }
        public void CheckOut(int id, int discount, float TongTien)
        {
            string query = "";
            query = $"Update HoaDon set TinhTrang = '1', GiamGia = {discount}, TongTien = {TongTien}, NgayKetThucHoaDon = getdate() where IDHoaDon='" + id + "'";
            DBMain.getInstance().MyExecuteNonQuery(query, CommandType.Text, ref err);
        }
        public void ThemHoaDonTheoBan(int idBan, ref string error)
        {
            int maxID = MaxIDHoaDon(ref error) + 1;
            //string strSQL = $"Insert into HoaDon values({maxID}, getdate(), null, {idBan}, 0, 0, 0)";
            DBMain.getInstance().MyExecuteNonQuery("Create_HoaDonTheoBan", CommandType.StoredProcedure, ref error, new SqlParameter("@IDHoaDon", maxID), new SqlParameter("@IDBanAn", idBan));
        }

        public int MaxIDHoaDon(ref string error)
        {
            string strSQL = $"Select Max(HoaDon.IDHoaDon) from HoaDon";
            return (int)DBMain.getInstance().FirstRowQuery(strSQL, CommandType.Text, ref error);
        }

        public void XoaHoaDon(int idHoaDon, ref string error)
        {
            //string query = "";
            //query = $"delete from HoaDon where idHoaDon = {idHoaDon} and TinhTrang = 0";
            DBMain.getInstance().MyExecuteNonQuery("Delete_HoaDon", CommandType.StoredProcedure, ref err, new SqlParameter("@idHoaDon", idHoaDon));
        }
    }
}
