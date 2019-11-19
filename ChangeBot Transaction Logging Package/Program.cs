using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeBot_Transaction_Logging_Package {//start namespace
    class Program {//start class
        struct CB_Log {//create structure and 
            public string transaction_number;
            public string date;
            public string time;
            public string cash_payment;
            public string card_vendor_and_amount;
            public string change_given;
        }//end struct
        static CB_Log bot_logs;
        static void Main(string[] args) {//start main function
            //variables
            string path = "";

            //pull date and time to local time and split on comma
            DateTime date1 = DateTime.UtcNow.ToLocalTime();
            string[] datetime = date1.ToString("MM-dd-yy, hh:mm:sstt").Split(',');
            
            //store info from args to bot_logs
            bot_logs.transaction_number      = args[0];
            bot_logs.date                    = datetime[0];
            bot_logs.time                    = datetime[1];
            bot_logs.cash_payment            = args[1];
            bot_logs.card_vendor_and_amount  = $"{args[2]} {args[3]} {args[4]}";
            bot_logs.change_given            = args[5];

            //run NameFileFunction to create a name for the file
            string filename = NameFileFunction(bot_logs);
            path = $"C:\\temp\\{filename}";            
            
            if(File.Exists(path)) {//if file exists - open file and add log info
                StreamWriter outfile = new StreamWriter(path, true);
                outfile.WriteLine($"{bot_logs.transaction_number}\t{bot_logs.date}\t{bot_logs.time}\t{bot_logs.cash_payment}\t\t{bot_logs.card_vendor_and_amount}\t\t{bot_logs.change_given}");
                outfile.Close();
            } else { //then create file - add title header - input first transaction
                StreamWriter outfile = new StreamWriter(path, true);
                outfile.WriteLine("TranAc#\tDate\t\tTime\t\tCashPayment\tVendor & CardPayment\tChangeGiven");               
                outfile.WriteLine($"{bot_logs.transaction_number}\t{bot_logs.date}\t{bot_logs.time}\t{bot_logs.cash_payment}\t\t{bot_logs.card_vendor_and_amount}\t\t{bot_logs.change_given}");
                outfile.Close();
            }//end if
        }//end main
        #region NAME FILE FUNCTION
        static string NameFileFunction(CB_Log log) {
            string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            string filename = "";
            string first = "";
            string second = "";
            string third = "";
            string ending = "Transactions.log";
            string[] brokendate = log.date.Split('-');
            int num_month = Convert.ToInt32(brokendate[0]);

            //loop to find the month
            for(int index = 0; index < months.Length; index++) {
                if(num_month - 1 == index) {
                    first = months[index] + "-";
                }//end if
            }//end for

            //set second and third to brokendate[1] and [2], add hyphen on end of each
            second = brokendate[1] + "-";
            third = brokendate[2] + "-";

            filename = first + second + third + ending;

            return filename;
        }//end function
        #endregion
    }//end class    
}//end namespace
