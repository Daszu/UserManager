using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Security.Principal;
using System.Security.Permissions;
using System.Data.SqlClient;
using Secure_Library;
using System.Threading;
namespace User_Manager
{

    public static class Global
    {
        //public static DataClasses1DataContext myConnection;
        public static User person;
        public static List<Permission> permissions;
        public static List<User> users;
        public static List<User> leftUsers = new List<User>();
        public static List<User> allUsers;
        public static List<Permission> allPermissions;
        public static List<Permission> leftPermissions = new List<Permission>();
        public static List<Permission> newGrpLeftPermissions = new List<Permission>();
        public static List<Permission> newGrpPermissions = new List<Permission>();
        public static List<Group> allGroups;
        public static String permErr = "Nie posiadasz uprawnień do wykonania tej czynności!";
        public static IUserCtx uctx; 
        public static DataClasses1DataContext DBConnect()
        {
            DataClasses1DataContext myConnection = new DataClasses1DataContext();
            return myConnection;
        }
    }

    static class Program
    {
        internal static void MyCommonExceptionHandlingMethod(object sender, ThreadExceptionEventArgs t)
        {
            string msg = System.DateTime.Now + " : Wystąpił krytyczny błąd wewnątrz aplikacji. Skontaktuj się z producentem.\n\n";
            msg += "Exception Message:\n";
            msg += t.Exception.Message + "\n";
            msg += "Stack Trace:\n";
            msg += t.Exception.StackTrace + "\n";

            if (t.Exception.InnerException != null)
            {
                Exception ie = t.Exception.InnerException;
                msg += "InnerException Message:\n";
                msg += t.Exception.Message + "\n";
            }
            else
                msg += "\n\r\n\rNo InnerException.\n";

            Clipboard.SetText(msg);
            msg += "\n\nKomunikat o błędzie został skopiowany do  schowka.";
            MessageBox.Show(msg);

            Application.Exit();

        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(MyCommonExceptionHandlingMethod);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.Run(new Form1());
        }
    }
}
