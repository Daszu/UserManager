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

namespace User_Manager
{
    public class Secure
    {
        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Roles.CREATE_USER)]
        public static void createUser(User newUser)
        {
            try
            {
                using (var conn = Global.DBConnect())
                {
                    conn.Users.InsertOnSubmit(newUser);
                    conn.SubmitChanges();
                    string msg = "Dodano nowego użytkownika:\n";
                    msg += newUser.Name;
                    MessageBox.Show(msg);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Roles.DELETE_USER)]
        public static void deleteUser(int uID)
        {
            try
            {
                using (var conn = Global.DBConnect())
                {
                    var del = (from u in conn.Users where u.UserID == uID select u).Single<User>();
                    conn.GetTable<User>().DeleteOnSubmit(del);
                    conn.SubmitChanges();
                    string msg = "Usunięto użytkownika:\n";
                    msg += del.Name;
                    MessageBox.Show(msg);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Roles.CREATE_GROUP)]
        public static void createGroup(Group newGrp, List<Permission> pe)
        {
            int newID;

            try
            {
                using (var conn = Global.DBConnect())
                {
                    conn.Groups.InsertOnSubmit(newGrp);
                    conn.SubmitChanges();
                    newID = newGrp.GroupID;
                    foreach (Permission p in pe)
                    {
                        GroupPermission gp = new GroupPermission();
                        gp.PermID = p.PermID;
                        gp.GroupID = newID;
                        conn.GroupPermissions.InsertOnSubmit(gp);
                    }
                    conn.SubmitChanges();
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Roles.DELETE_GROUP)]
        public static void deleteGroup(int gID)
        {
            try
            {
                using (var conn = Global.DBConnect())
                {
                    var del = (from g in conn.Groups where g.GroupID == gID select g).Single<Group>();
                    conn.GetTable<Group>().DeleteOnSubmit(del);
                    conn.SubmitChanges();
                    string msg = "Usunięto grupę:\n";
                    msg += del.Name;
                    MessageBox.Show(msg);
                }

            }
            catch (Exception exc)
            {
                throw exc;
            }

        }

        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Roles.USER_PERMISSION)]
        public static void userPermissions(int uID, List<Permission> pe)
        {
            try
            {
                using (var conn = Global.DBConnect())
                {
                    var obj = conn.UserPermissions;
                    obj.Where(u => u.UserID == uID).ToList().ForEach(obj.DeleteOnSubmit);
                    foreach (Permission p in pe)
                    {
                        UserPermission up = new UserPermission();
                        up.PermID = p.PermID;
                        up.UserID = uID;
                        conn.UserPermissions.InsertOnSubmit(up);
                    }
                    conn.SubmitChanges();
                    string msg = "Zapisano uprawnienia.";
                    MessageBox.Show(msg);
                }

            }
            catch (Exception exc)
            {
                throw exc;
            }

        }

        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Roles.GROUP_PERMISSION)]
        public static void groupPermissions(int gID, List<Permission> gp)
        {
            try
            {
                using (var conn = Global.DBConnect())
                {
                    var obj = conn.GroupPermissions;
                    obj.Where(g => g.GroupID == gID).ToList().ForEach(obj.DeleteOnSubmit);
                    foreach (Permission p in gp)
                    {
                        GroupPermission np = new GroupPermission();
                        np.PermID = p.PermID;
                        np.GroupID = gID;
                        conn.GroupPermissions.InsertOnSubmit(np);
                    }
                    conn.SubmitChanges();
                    string msg = "Zapisano uprawnienia.";
                    MessageBox.Show(msg);
                }

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        [PrincipalPermissionAttribute(SecurityAction.Demand, Role = Roles.USER_GROUP)]
        public static void usersGroup(int gID, List<User> nu)
        {
            try
            {
                using (var conn = Global.DBConnect())
                {
                    var obj = conn.GroupUsers;
                    obj.Where(g => (g.GroupId == gID) && (g.UserID != 2)).ToList().ForEach(obj.DeleteOnSubmit);
                    foreach (User u in nu)
                    {
                        GroupUser gu = new GroupUser();
                        gu.UserID = u.UserID;
                        gu.GroupId = gID;
                        conn.GroupUsers.InsertOnSubmit(gu);
                    }
                    conn.SubmitChanges();
                    string msg = "Zapisano użytkowników.";
                    MessageBox.Show(msg);
                }

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
