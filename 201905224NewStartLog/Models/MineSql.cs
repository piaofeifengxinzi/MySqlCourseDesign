using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace _201905224NewStartLog
{
    //静态类的方法和成员，全局可用，还省空间
    public class MineSql
    {
        //创建连接数据库字符串，这里的登录会给连接的管理员不同的权限，用不同的MySQL用户来区分
        private static string LogString = "server=localhost;user id=root;password=zjlcxkf@14;database=test;";
        //连接数据库的固定用法，不会？！！！，自己查去,后期要将这个写在方法里，以解决登陆时的授权问题
        private static MySqlConnection MySqlConnection = new MySqlConnection(LogString);
        //这是命令字符串，就是一个简单的命令字符串
        private static string CommandString = "select *from nation";
        //还记得吗，公有方法外部可见，返回布尔值以增强程序的健壮性，差点忘了，查找等一些耗时操作，要await
        public static bool ChuShiHua()
        {
            //用try防止程序崩溃，反正一直转圈圈也不能让用户看到电脑蓝屏，软件闪退
            try
            {
                MySqlConnection.Open();
                return true;
            }catch(Exception e)
            {
                e.GetBaseException();
                return false;
            }
        }


        //这个方法是返回查找的对象的，模板编程真好用
        public static List<string> GetData(string commandString, int num)
        {
            List<string> vs = new List<string>();
            try
            {
                //这一句算是向sql输入指令数据
                //MySqlCommand mySqlCommand = new MySqlCommand(CommandString, MySqlConnection);
                MySqlCommand mySqlCommand = new MySqlCommand(commandString, MySqlConnection);
                //这一句是将数据库的数据放入缓冲区，不知道为啥
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                //将数据显示出来，while循环是咋实现的呢,Read方法是有元组输出就为真，哈哈哈是个人都能看出来
                while (mySqlDataReader.Read())
                {
                    //这个不用解释了吧，太基本的东西了，就是不知道怎么实现的，只会用，感觉比Java好用一点，微软真香
                    string first = mySqlDataReader.GetInt32(0).ToString();
                    string second = mySqlDataReader.GetString(1).ToString();
                    string third = mySqlDataReader.GetDateTime(2).ToString();
                    string fouth = mySqlDataReader.GetString(3);
                    string fifth = mySqlDataReader.GetString(4);
                    //这个Add似曾相识的赶脚呢，Python里的列表用的是append，反正好用，就是真香
                    //num有几个，就添加几个add
                    vs.Add(first);
                    vs.Add(second);
                    vs.Add(third);
                    vs.Add(fouth);
                    vs.Add(fifth);
                }
            }
            catch(Exception e)
            {
                e.GetBaseException();
                //这里出错不给任何提示，如果有问题，不是我的郭
            }
            return vs;
        }
    }
}
