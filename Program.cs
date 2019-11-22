using System;
using System.Collections.Generic;
using System.IO;
using ConsoleApp1.Model;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        /// <summary>
        ///     讀取檔案路徑
        /// </summary>
        public static string Input_File_Path = "C:\\DMP\\";
        /// <summary>
        ///     讀取檔案檔名
        /// </summary>
        public static string Input_File_Name = string.Format("cookie_model_sample_{0}_1.json", DateTime.Today.ToString("yyyyMMdd"));

        /// <summary>
        ///     寫入檔案路徑
        /// </summary>
        public static string Output_File_Path = "C:\\DMP\\output\\";
        /// <summary>
        ///     寫入檔案檔名
        /// </summary>
        public static string Output_File_Name = string.Format("ftp_dmp_[dpid]_[{0}].sync", DateTime.Today.ToString("yyyyMMdd"));


        static void Main(string[] args)
        {
            try
            {
                if (File.Exists(Input_File_Path + Input_File_Name))
                {
                    //待寫入資料
                    List<DMPtags_Model> DMPtags = new List<DMPtags_Model>();

                    #region 檔案讀取
                    try
                    {
                        using (StreamReader sr = new StreamReader(Input_File_Path + Input_File_Name))
                        {
                            List<Cookie_Model> cookies = new List<Cookie_Model>();

                            string line;
                            while ((line = sr.ReadLine()) != null)
                            {
                                Cookie_Model Cookie_Model_temp = new Cookie_Model();
                                Cookie_Model_temp = JsonConvert.DeserializeObject<Cookie_Model>(line);

                                DMPtags_Model DMPtags_temp = new DMPtags_Model();
                                DMPtags_temp.cookie_id = Cookie_Model_temp.cookie_id;
                                DMPtags_temp.dmptags = Cookie_Model_temp.l1_product + '|' + Cookie_Model_temp.l2_product;
                                DMPtags.Add(DMPtags_temp);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        //檔案讀取失敗
                    }
                    #endregion

                    #region 檔案寫入
                    try
                    {
                        if (DMPtags != null && DMPtags.Count > 0)
                        {
                            using (StreamWriter OutputFile = new StreamWriter(Output_File_Path + Output_File_Name))
                            {
                                foreach (DMPtags_Model tag in DMPtags)
                                {
                                    string Line = string.Format("{0}" + '\t' + @"""dmptags""=" + @"""{1}""", tag.cookie_id, tag.dmptags);
                                    OutputFile.WriteLine(Line);
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        //檔案寫入失敗
                    }
                    #endregion
                }
                else
                {
                    ///讀取檔案不存在
                }
            }
            catch (Exception ex)
            {
                //Do Nothing
            }
        }

    }
}
