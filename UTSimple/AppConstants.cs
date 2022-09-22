using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSimple
{
    public static class AppConstants
    {
        public static string SrcBaseFolderPath { get; set; } = @"C:\WebPos\SaturnEMoney\WPSaturnEMoney";

        public static string SrcLogFolderPath { get; set; } = "Log";
        public static string SrcTransFolderPath { get; set; } = "transaction_data";

        public static string SrcConfigFolderPath { get; set; } = "Config";

        public static string SrcSndFolderPath { get; set; } = "Snd";

        public static string SrcPosFolderPath { get; set; } = "Pos";

        public static string DesBaseFolderPath { get; set; } = @"D:\workspace\Hei\UT\HEI_SELF-files-Common_iD\HEI_SELF-82 001 POSレジアプリ連携－アプリ起動\実施内容1（カスタマー画面ON）";

    }
}
