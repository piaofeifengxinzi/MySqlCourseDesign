using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _201905224NewStartLog;
namespace _201905224NewStartLog.Models
{
    public class People
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Sex { set; get; }
        public string Age { get; set; }
        public string Picture { get; set; }
    }
    //这个类相当于与数据库查询的结果对接
    //现在这个类只是一个验证性的数据示例
    public class PeopleManager
    {
        private static string command = "select *from staff";
        private static List<People> people = new List<People>();
        private static List<string> data = new List<string>();
        public static List<People> GetPeople()
        {
            MineSql.ChuShiHua();
            data = MineSql.GetData(command, 5);
            int i = 0;
            while(i<data.Count)
            {
                people.Add(new People { Id = data[i], Name = data[i + 1], Age = data[i + 2], Sex = data[i + 3], Picture = "Assets/Assets2/"+data[i + 4]+".jpg" });
                i += 5;
            }
            return people;
        }
    }
}
