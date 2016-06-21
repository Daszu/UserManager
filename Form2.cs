using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Security.Principal;
using System.Security.Permissions;
using System.Data.SqlClient;
using Secure_Library;

namespace User_Manager
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            groupBox1.Enabled = Global.uctx.HasPermission(Roles.CREATE_USER);
            groupBox2.Enabled = Global.uctx.HasPermission(Roles.CREATE_GROUP);
            button14.Enabled = Global.uctx.HasPermission(Roles.DELETE_USER);
            groupBox3.Enabled = Global.uctx.HasPermission(Roles.USER_PERMISSION);
            groupBox4.Enabled = Global.uctx.HasPermission(Roles.GROUP_PERMISSION);
            groupBox5.Enabled = Global.uctx.HasPermission(Roles.USER_GROUP);
            button5.Enabled = Global.uctx.HasPermission(Roles.DELETE_GROUP);

            textBox1.Text = Global.uctx.UName;
            textBox8.Text = Global.uctx.FName;
            textBox9.Text = Global.uctx.LName;

            using (var conn = Global.DBConnect())
            {
                var groups = conn.Groups;
                Global.allGroups = groups.ToList<Group>();
                listBox10.DataSource = groups;
                listBox10.DisplayMember = "Name";
                listBox10.ValueMember = "GroupID";
                listBox10.SelectedIndex = -1;

                var permissions = conn.Permissions;
                Global.leftPermissions = permissions.ToList<Permission>();
                Global.newGrpLeftPermissions = permissions.ToList<Permission>();
                Global.allPermissions = permissions.ToList<Permission>();
                listBox11.DataSource = permissions;
                listBox11.DisplayMember = "DispName";
                listBox11.ValueMember = "PermID";

                var users = conn.Users;
                Global.users = users.ToList<User>();
                Global.allUsers = users.ToList<User>();
                listBox1.DataSource = users;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "UserID";
                listBox1.SelectedIndex = -1;
            }

        }

        public void refreshForm()
        {
            Form2 NewForm = new Form2();
            NewForm.Show();
            this.Dispose(false);
        }

        //EDYTUJ SWOJE KONTO
        private void button1_Click(object sender, EventArgs e)
        {
            bool pass = false, name = false, fname = false, lname = false;
            string msg = "";
            string currentName = Global.uctx.UName;

            if(textBox1.Text != "" && textBox8.Text != "" && textBox9.Text != ""){
                if(textBox1.Text != Global.uctx.UName)
                {
                    try
                    {
                        using (var conn = Global.DBConnect())
                        {
                            foreach (User usr in conn.Users)
                            {
                                if(usr.Name == textBox1.Text)
                                {
                                    msg += "Nazwa jest zajęta przez innego użytkownika.\n";
                                    name = true;
                                    break;
                                }
                            }
                        }
                    }
                    catch(Exception exc)
                    {
                        throw exc;
                    }

                    if(!name)
                    {
                        Global.uctx.UName = textBox1.Text;
                        name = true;
                    } else
                        name = false;

                }

                if (textBox8.Text != Global.uctx.FName)
                {
                    Global.uctx.FName = textBox8.Text;
                    fname = true;
                }

                if (textBox9.Text != Global.uctx.LName)
                {
                    Global.uctx.LName = textBox9.Text;
                    lname = true;
                }
            } else
                msg += "Nazwa, imię, nazwisko nie mogą być puste.\n";

            if(textBox2.Text != "")
            {
                byte[] encodedPassword = new UTF8Encoding().GetBytes(textBox2.Text);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

                if (encoded == Global.uctx.Pass && textBox3.Text != "")
                {
                    encodedPassword = new UTF8Encoding().GetBytes(textBox3.Text);
                    hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                    encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
                    Global.uctx.Pass = encoded;
                    pass = true;
                }
            }
           
            if (name || fname || lname || pass)
            {
                User user;
                try
                {
                    using (var conn = Global.DBConnect())
                    {
                        user = conn.Users.Single(u => u.Name == currentName);
                        if (name) user.Name = Global.uctx.UName;
                        if (fname) user.FName = Global.uctx.FName;
                        if (lname) user.LName = Global.uctx.LName;
                        if (pass) user.Pass = Global.uctx.Pass;
                        conn.SubmitChanges();
                    }
                    msg += "Zmiany zostały zapisane.\n";
                    
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }
            else
            {
                msg += "Wprowadź poprawne dane do zapisu.";
            }
           
            MessageBox.Show(msg);

        }

        private void wyjścieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //WYSWIETL UPRAWNIENIA UZYTKOWNIKA
        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedValue != null)
            {
                if (listBox1.Items.Count > 0)
                {
                    var ID = Convert.ToInt32(listBox1.SelectedValue.ToString());
                    if (ID == 2 || (!Thread.CurrentPrincipal.IsInRole(Roles.USER_PERMISSION))) groupBox3.Enabled = false;
                    else groupBox3.Enabled = true;
                    User selectedUser = new User();
                    foreach (User u in Global.allUsers)
                    {
                        if (u.UserID == ID)
                        {
                            selectedUser = u;
                            break;
                        }
                    }
                    try
                    {
                        using (var conn = Global.DBConnect())
                        {
                            var usrPermissions = from up in conn.UserPermissions join p in conn.Permissions on up.PermID equals p.PermID where up.UserID == selectedUser.UserID select p;
                            var usrGrpPermissions = from gu in conn.GroupUsers join gp in conn.GroupPermissions on gu.GroupId equals gp.GroupID join p in conn.Permissions on gp.PermID equals p.PermID where gu.UserID == selectedUser.UserID select p;
                            var leftPermissions = (from ap in conn.Permissions select ap).Except(usrGrpPermissions).Except(usrPermissions);
                            Global.permissions = usrPermissions.ToList<Permission>();
                            Global.leftPermissions = leftPermissions.ToList<Permission>();
                            listBox4.DataSource = usrGrpPermissions;
                            listBox4.DisplayMember = "DispName";
                        }
                    }
                    catch (Exception exc)
                    {
                        throw exc;
                    }

                    listBox2.DataSource = Global.permissions;
                    listBox2.DisplayMember = "DispName";
                    listBox2.ValueMember = "PermID";
                    listBox3.DataSource = Global.leftPermissions;
                    listBox3.DisplayMember = "DispName";
                    listBox3.ValueMember = "PermID";

                }
            }
        }

        //UTWORZ UZYTKOWNIKA
        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {
                if (textBox5.Text == textBox6.Text)
                {
                    byte[] encodedPassword = new UTF8Encoding().GetBytes(textBox5.Text);
                    byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                    string encoded = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

                    User newUser = new User();
                    newUser.Name = textBox4.Text;
                    newUser.Pass = encoded;
                    newUser.Active = true;
                    try
                    {
                        Secure.createUser(newUser);
                        listBox1.Refresh();
                    }
                    catch (Exception exc)
                    {
                        throw exc;
                    }
                    
                }
                else
                    MessageBox.Show("Hasło musi być identyczne w obu polach!");
            }
            else
                MessageBox.Show("Wypełnij odpowiednie pola!");

        }

        //USUN UZYTKOWNIKA
        private void button14_Click(object sender, EventArgs e)
        {
            var uID = Convert.ToInt32(listBox1.SelectedValue.ToString());

            try
            {
                Secure.deleteUser(uID);
                var delUser = (from au in Global.allUsers where au.UserID == uID select au);
                Global.allUsers = Global.allUsers.Except(delUser).ToList<User>();
                listBox1.DataSource = Global.allUsers;
                listBox1.DisplayMember = "Name";
                listBox1.ValueMember = "UserID";
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }


        //NADAJ UPRAWNIENIE UZYTKOWNIKOWI
        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox3.SelectedItem != null)
            {
                var ID = Convert.ToInt32(listBox3.SelectedValue.ToString());
                Global.permissions.Add((from ap in Global.allPermissions where ap.PermID == ID select ap).Single<Permission>());
                Global.leftPermissions.RemoveAll(lp => lp.PermID == ID);
                listBox2.DataSource = null;
                listBox2.DataSource = Global.permissions;
                listBox2.DisplayMember = "DispName";
                listBox2.ValueMember = "PermID";
                listBox3.DataSource = null;
                listBox3.DataSource = Global.leftPermissions;
                listBox3.DisplayMember = "DispName";
                listBox3.ValueMember = "PermID";
            }
        }

        //ANULUJ UPRAWNIENIE UZYTKOWNIKOWI
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                var ID = Convert.ToInt32(listBox2.SelectedValue.ToString());
                Global.leftPermissions.Add((from p in Global.permissions where p.PermID == ID select p).Single<Permission>());
                Global.permissions.RemoveAll(p => p.PermID == ID);
                listBox2.DataSource = null;
                listBox2.DataSource = Global.permissions;
                listBox2.DisplayMember = "DispName";
                listBox2.ValueMember = "PermID";
                listBox3.DataSource = null;
                listBox3.DataSource = Global.leftPermissions;
                listBox3.DisplayMember = "DispName";
                listBox3.ValueMember = "PermID";
            }
        }

        //NADAJ UPRAWNIENIE NOWEJ GRUPIE
        private void button12_Click(object sender, EventArgs e)
        {
            if (listBox11.SelectedItem != null)
            {
                var ID = Convert.ToInt32(listBox11.SelectedValue.ToString()); 
                Global.newGrpPermissions.Add((from ap in Global.allPermissions where ap.PermID == ID select ap).Single<Permission>());
                Global.newGrpLeftPermissions.RemoveAll(lp => lp.PermID == ID);
                listBox12.DataSource = null;
                listBox12.DataSource = Global.newGrpPermissions;
                listBox12.DisplayMember = "DispName";
                listBox12.ValueMember = "PermID";
                listBox11.DataSource = null;
                listBox11.DataSource = Global.newGrpLeftPermissions;
                listBox11.DisplayMember = "DispName";
                listBox11.ValueMember = "PermID";
            }  
        }

        //ANULUJ UPRAWNIENIE NOWEJ GRUPIE
        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox12.SelectedItem != null)
            {
                var ID = Convert.ToInt32(listBox12.SelectedValue.ToString());              
                Global.newGrpLeftPermissions.Add((from ap in Global.allPermissions where ap.PermID == ID select ap).Single<Permission>());
                Global.newGrpPermissions.RemoveAll(lp => lp.PermID == ID);
                listBox12.DataSource = null;
                listBox12.DataSource = Global.newGrpPermissions;
                listBox12.DisplayMember = "DispName";
                listBox12.ValueMember = "PermID";
                listBox11.DataSource = null;
                listBox11.DataSource = Global.newGrpLeftPermissions;
                listBox11.DisplayMember = "DispName";
                listBox11.ValueMember = "PermID";
            }  
        }

        //UTWORZ NOWA GRUPE
        private void button13_Click(object sender, EventArgs e)
        {
            string name = textBox7.Text;
            Group newGrp = new Group();
            newGrp.Name = name;
            newGrp.Active = true;

            try
            {
                Secure.createGroup(newGrp, Global.newGrpPermissions);
                Global.newGrpPermissions.Clear();
                Global.newGrpLeftPermissions = Global.allPermissions;
                this.refreshForm();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        //ZAPISZ UPRAWNIENIA UZYTKOWNIKA
        private void button4_Click(object sender, EventArgs e)
        {
            var uID = Convert.ToInt32(listBox1.SelectedValue.ToString());

            try
            {
                Secure.userPermissions(uID, Global.permissions);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        //WYSWIETL UZYTKOWNIKOW I UPRAWNIENIA GRUPY
        private void listBox10_Click(object sender, EventArgs e)
        {
            if (listBox10.SelectedValue != null)
            {
                if (listBox10.Items.Count > 0)
                {
                    var ID = Convert.ToInt32(listBox10.SelectedValue.ToString());
                    Group selectedGroup = new Group();
                    foreach (Group g in Global.allGroups)
                    {
                        if (g.GroupID == ID)
                        {
                            selectedGroup = g;
                            break;
                        }
                    }
                    try
                    {
                        using (var conn = Global.DBConnect())
                        {
                            var grpPermissions = from gp in conn.GroupPermissions join p in conn.Permissions on gp.PermID equals p.PermID where gp.GroupID == selectedGroup.GroupID select p;
                            var leftPermissions = (from ap in conn.Permissions select ap).Except(grpPermissions);
                            Global.permissions = grpPermissions.ToList<Permission>();
                            Global.leftPermissions = leftPermissions.ToList<Permission>();

                            var grpUsers = from gu in conn.GroupUsers join u in conn.Users on gu.UserID equals u.UserID where gu.GroupId == selectedGroup.GroupID select u;
                            var leftUsers = (from au in conn.Users select au).Except(grpUsers);
                            Global.users = grpUsers.ToList<User>();
                            Global.leftUsers = leftUsers.ToList<User>();
                        }

                        listBox9.DataSource = Global.permissions;
                        listBox9.DisplayMember = "DispName";
                        listBox9.ValueMember = "PermID";
                        listBox8.DataSource = Global.leftPermissions;
                        listBox8.DisplayMember = "DispName";
                        listBox8.ValueMember = "PermID";
                        listBox7.DataSource = Global.users;
                        listBox7.DisplayMember = "Name";
                        listBox7.ValueMember = "UserID";
                        listBox6.DataSource = Global.leftUsers;
                        listBox6.DisplayMember = "Name";
                        listBox6.ValueMember = "UserID";
                    }
                    catch(Exception exc)
                    {
                        throw exc;
                    }


                }
            }
        }

        //NADAJ UPRAWNIENIE GRUPIE
        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox8.SelectedItem != null)
            {
                var ID = Convert.ToInt32(listBox8.SelectedValue.ToString());                
                Global.permissions.Add((from ap in Global.allPermissions where ap.PermID == ID select ap).Single<Permission>());
                Global.leftPermissions.RemoveAll(lp => lp.PermID == ID);
                listBox9.DataSource = null;
                listBox9.DataSource = Global.permissions;
                listBox9.DisplayMember = "DispName";
                listBox9.ValueMember = "PermID";
                listBox8.DataSource = null;
                listBox8.DataSource = Global.leftPermissions;
                listBox8.DisplayMember = "DispName";
                listBox8.ValueMember = "PermID";
            } 
        }

        //ANULUJ UPRAWNIENIE GRUPIE
        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox9.SelectedItem != null)
            {
                var ID = Convert.ToInt32(listBox9.SelectedValue.ToString());
                Global.leftPermissions.Add((from p in Global.permissions where p.PermID == ID select p).Single<Permission>());
                Global.permissions.RemoveAll(p => p.PermID == ID);
                listBox9.DataSource = null;
                listBox9.DataSource = Global.permissions;
                listBox9.DisplayMember = "DispName";
                listBox9.ValueMember = "PermID";
                listBox8.DataSource = null;
                listBox8.DataSource = Global.leftPermissions;
                listBox8.DisplayMember = "DispName";
                listBox8.ValueMember = "PermID";
            }
        }

        //PRZYPISZ UZYTKOWNIKA GRUPIE
        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox6.SelectedItem != null)
            {
                var ID = Convert.ToInt32(listBox6.SelectedValue.ToString());
                Global.users.Add((from au in Global.allUsers where au.UserID == ID select au).Single<User>());
                Global.leftUsers.RemoveAll(lu => lu.UserID == ID);
                listBox7.DataSource = null;
                listBox7.DataSource = Global.users;
                listBox7.DisplayMember = "Name";
                listBox7.ValueMember = "UserID";
                listBox6.DataSource = null;
                listBox6.DataSource = Global.leftUsers;
                listBox6.DisplayMember = "Name";
                listBox6.ValueMember = "UserID";
            }
        }

        //USUN UZYTKOWNIKA Z GRUPY
        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox7.SelectedItem != null)
            {
                var ID = Convert.ToInt32(listBox7.SelectedValue.ToString());
                Global.leftUsers.Add((from au in Global.allUsers where au.UserID == ID select au).Single<User>());
                Global.users.RemoveAll(u => u.UserID == ID);
                listBox7.DataSource = null;
                listBox7.DataSource = Global.users;
                listBox7.DisplayMember = "Name";
                listBox7.ValueMember = "UserID";
                listBox6.DataSource = null;
                listBox6.DataSource = Global.leftUsers;
                listBox6.DisplayMember = "Name";
                listBox6.ValueMember = "UserID";
            }
        }

        //USUN GRUPE
        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox10.SelectedItem != null)
            {
                var gID = Convert.ToInt32(listBox10.SelectedValue.ToString());
                try
                {
                    Secure.deleteGroup(gID);
                    var delGroup = (from ag in Global.allGroups where ag.GroupID == gID select ag);
                    Global.allGroups = Global.allGroups.Except(delGroup).ToList<Group>();
                    listBox10.DataSource = Global.allGroups;
                    listBox10.DisplayMember = "Name";
                    listBox10.ValueMember = "GroupID";
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }

        }

        private void wylogujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.CurrentPrincipal = null;
            this.Hide();
            var loginForm = new Form1();
            loginForm.Closed += (s, args) => this.Close();
            loginForm.Show();
        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("User Manager 1.0 final\n\nAndrzejewski Marcin\nKopel Szymon\nMańka Bartłomiej\nSkubis Miłosz");
        }

        //ZAPISZ UPRAWNIENIA GRUPY
        private void button15_Click(object sender, EventArgs e)
        {
            if (listBox10.SelectedItem != null)
            {
                var gID = Convert.ToInt32(listBox10.SelectedValue.ToString());

                try
                {
                    Secure.groupPermissions(gID, Global.permissions);
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }
        }

        //ZAPISZ UZYTKOWNIKOW GRUPY
        private void button16_Click(object sender, EventArgs e)
        {
            if (listBox10.SelectedItem != null)
            {
                var gID = Convert.ToInt32(listBox10.SelectedValue.ToString());

                try
                {
                    Secure.usersGroup(gID, Global.users);
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }
        }

    }
}
