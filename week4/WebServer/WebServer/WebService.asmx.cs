using System;
using System.IO;
using System.Web.Services;

namespace WebServer
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebService : System.Web.Services.WebService
    {
        private double CalCov(double[] a, double[] b)
        {
            int n = a.Length;
            double sum_a = 0, sum_b = 0, sum_ab = 0;

            for (int i = 0; i < n; i++)
            {
                sum_a += a[i];
                sum_b += b[i];
                sum_ab += a[i] * b[i];
            }

            return sum_ab / n - (sum_a / n) * (sum_b / n);
        }

        [WebMethod(Description = "第四次实习：计算两个数组的协方差")]
        public string CalCovariance(string x, string y)
        {
            string[] str_x = x.Split(',');
            string[] str_y = y.Split(',');
            double[] dbl_x = new double[str_x.Length];
            double[] dbl_y = new double[str_y.Length];

            // 检验输入合法性
            if (str_x.Length <= 0 || str_x.Length != str_y.Length)
            {
                throw new Exception("Please input correct arrays!");
            }

            // 转换数据类型
            for (int i = 0; i < str_x.Length; i++)
            {
                dbl_x[i] = Convert.ToDouble(str_x[i]);
                dbl_y[i] = Convert.ToDouble(str_y[i]);
            }

            return CalCov(dbl_x, dbl_y).ToString();
        }

        [WebMethod(Description = "第四次实习：提取文本中的经纬度信息，并计算经纬度坐标的最大值")]
        public string CalMaxLnglat(string path)
        {
            double bufLng, bufLat, maxLng = -180, maxLat = -90;

            // 将相对路径转换为绝对路径
            string rootPath = Server.MapPath("./");
            string filePath = rootPath + path;

            // 读取坐标文件
            string[] contents = File.ReadAllLines(filePath);
            int n = contents.Length;  // 减少数组长度计算次数，提高遍历效率

            // 计算经纬度坐标的最大值
            for (int i = 1; i < n; i++)
            {
                string[] buffer = contents[i].Split(',');
                bufLng = Convert.ToDouble(buffer[3]);
                bufLat = Convert.ToDouble(buffer[4]);
                maxLng = bufLng > maxLng ? bufLng : maxLng;
                maxLat = bufLat > maxLat ? bufLat : maxLat;
            }

            return "Max longitude: " + maxLng + "; Max latitude: " + maxLat;
        }
    }
}
