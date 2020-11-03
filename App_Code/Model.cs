using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Model 的摘要说明
/// </summary>
using System.Data;


namespace vbsunyuanqing.App_Code
{
    public class Movie
    {

        public int id { get; set; }

        public decimal time { get; set; }

    }
    public class Province
    {
        public int ProvinceID { get; set; }

        public string ProvinceName { get; set; }
    }
    //public class User_Info
    //{
    //    public int CityID { get; set; }

    //    public string CityName { get; set; }

    //    public int ProvinceID { get; set; }

    //}
    public class User_Info
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Pwd { get; set; }

    }
  
}