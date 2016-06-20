﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace User_Manager
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="USERMANAGEMENT")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertGroupPermission(GroupPermission instance);
    partial void UpdateGroupPermission(GroupPermission instance);
    partial void DeleteGroupPermission(GroupPermission instance);
    partial void InsertGroup(Group instance);
    partial void UpdateGroup(Group instance);
    partial void DeleteGroup(Group instance);
    partial void InsertGroupUser(GroupUser instance);
    partial void UpdateGroupUser(GroupUser instance);
    partial void DeleteGroupUser(GroupUser instance);
    partial void InsertUserPermission(UserPermission instance);
    partial void UpdateUserPermission(UserPermission instance);
    partial void DeleteUserPermission(UserPermission instance);
    partial void InsertPermission(Permission instance);
    partial void UpdatePermission(Permission instance);
    partial void DeletePermission(Permission instance);
    partial void InsertUser(User instance);
    partial void UpdateUser(User instance);
    partial void DeleteUser(User instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::User_Manager.Properties.Settings.Default.USERMANAGEMENTConnectionString2, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<GroupPermission> GroupPermissions
		{
			get
			{
				return this.GetTable<GroupPermission>();
			}
		}
		
		public System.Data.Linq.Table<Group> Groups
		{
			get
			{
				return this.GetTable<Group>();
			}
		}
		
		public System.Data.Linq.Table<GroupUser> GroupUsers
		{
			get
			{
				return this.GetTable<GroupUser>();
			}
		}
		
		public System.Data.Linq.Table<UserPermission> UserPermissions
		{
			get
			{
				return this.GetTable<UserPermission>();
			}
		}
		
		public System.Data.Linq.Table<Permission> Permissions
		{
			get
			{
				return this.GetTable<Permission>();
			}
		}
		
		public System.Data.Linq.Table<User> Users
		{
			get
			{
				return this.GetTable<User>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.GroupPermission")]
	public partial class GroupPermission : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _GroupID;
		
		private int _PermID;
		
		private System.DateTime _AddDate;
		
		private EntityRef<Group> _Group;
		
		private EntityRef<Permission> _Permission;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnGroupIDChanging(int value);
    partial void OnGroupIDChanged();
    partial void OnPermIDChanging(int value);
    partial void OnPermIDChanged();
    partial void OnAddDateChanging(System.DateTime value);
    partial void OnAddDateChanged();
    #endregion
		
		public GroupPermission()
		{
			this._Group = default(EntityRef<Group>);
			this._Permission = default(EntityRef<Permission>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GroupID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int GroupID
		{
			get
			{
				return this._GroupID;
			}
			set
			{
				if ((this._GroupID != value))
				{
					if ((this._Group.HasLoadedOrAssignedValue || this._Permission.HasLoadedOrAssignedValue))
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnGroupIDChanging(value);
					this.SendPropertyChanging();
					this._GroupID = value;
					this.SendPropertyChanged("GroupID");
					this.OnGroupIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PermID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int PermID
		{
			get
			{
				return this._PermID;
			}
			set
			{
				if ((this._PermID != value))
				{
					this.OnPermIDChanging(value);
					this.SendPropertyChanging();
					this._PermID = value;
					this.SendPropertyChanged("PermID");
					this.OnPermIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AddDate", DbType="Date NOT NULL")]
		public System.DateTime AddDate
		{
			get
			{
				return this._AddDate;
			}
			set
			{
				if ((this._AddDate != value))
				{
					this.OnAddDateChanging(value);
					this.SendPropertyChanging();
					this._AddDate = value;
					this.SendPropertyChanged("AddDate");
					this.OnAddDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Group_GroupPermission", Storage="_Group", ThisKey="GroupID", OtherKey="GroupID", IsForeignKey=true)]
		public Group Group
		{
			get
			{
				return this._Group.Entity;
			}
			set
			{
				Group previousValue = this._Group.Entity;
				if (((previousValue != value) 
							|| (this._Group.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Group.Entity = null;
						previousValue.GroupPermissions.Remove(this);
					}
					this._Group.Entity = value;
					if ((value != null))
					{
						value.GroupPermissions.Add(this);
						this._GroupID = value.GroupID;
					}
					else
					{
						this._GroupID = default(int);
					}
					this.SendPropertyChanged("Group");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Permission_GroupPermission", Storage="_Permission", ThisKey="GroupID", OtherKey="PermID", IsForeignKey=true)]
		public Permission Permission
		{
			get
			{
				return this._Permission.Entity;
			}
			set
			{
				Permission previousValue = this._Permission.Entity;
				if (((previousValue != value) 
							|| (this._Permission.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Permission.Entity = null;
						previousValue.GroupPermissions.Remove(this);
					}
					this._Permission.Entity = value;
					if ((value != null))
					{
						value.GroupPermissions.Add(this);
						this._GroupID = value.PermID;
					}
					else
					{
						this._GroupID = default(int);
					}
					this.SendPropertyChanged("Permission");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Groups")]
	public partial class Group : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _GroupID;
		
		private string _Name;
		
		private bool _Active;
		
		private EntitySet<GroupPermission> _GroupPermissions;
		
		private EntitySet<GroupUser> _GroupUsers;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnGroupIDChanging(int value);
    partial void OnGroupIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnActiveChanging(bool value);
    partial void OnActiveChanged();
    #endregion
		
		public Group()
		{
			this._GroupPermissions = new EntitySet<GroupPermission>(new Action<GroupPermission>(this.attach_GroupPermissions), new Action<GroupPermission>(this.detach_GroupPermissions));
			this._GroupUsers = new EntitySet<GroupUser>(new Action<GroupUser>(this.attach_GroupUsers), new Action<GroupUser>(this.detach_GroupUsers));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GroupID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int GroupID
		{
			get
			{
				return this._GroupID;
			}
			set
			{
				if ((this._GroupID != value))
				{
					this.OnGroupIDChanging(value);
					this.SendPropertyChanging();
					this._GroupID = value;
					this.SendPropertyChanged("GroupID");
					this.OnGroupIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Active", DbType="Bit NOT NULL")]
		public bool Active
		{
			get
			{
				return this._Active;
			}
			set
			{
				if ((this._Active != value))
				{
					this.OnActiveChanging(value);
					this.SendPropertyChanging();
					this._Active = value;
					this.SendPropertyChanged("Active");
					this.OnActiveChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Group_GroupPermission", Storage="_GroupPermissions", ThisKey="GroupID", OtherKey="GroupID")]
		public EntitySet<GroupPermission> GroupPermissions
		{
			get
			{
				return this._GroupPermissions;
			}
			set
			{
				this._GroupPermissions.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Group_GroupUser", Storage="_GroupUsers", ThisKey="GroupID", OtherKey="GroupId")]
		public EntitySet<GroupUser> GroupUsers
		{
			get
			{
				return this._GroupUsers;
			}
			set
			{
				this._GroupUsers.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_GroupPermissions(GroupPermission entity)
		{
			this.SendPropertyChanging();
			entity.Group = this;
		}
		
		private void detach_GroupPermissions(GroupPermission entity)
		{
			this.SendPropertyChanging();
			entity.Group = null;
		}
		
		private void attach_GroupUsers(GroupUser entity)
		{
			this.SendPropertyChanging();
			entity.Group = this;
		}
		
		private void detach_GroupUsers(GroupUser entity)
		{
			this.SendPropertyChanging();
			entity.Group = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.GroupUser")]
	public partial class GroupUser : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserID;
		
		private int _GroupId;
		
		private System.DateTime _AddDate;
		
		private EntityRef<Group> _Group;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIDChanging(int value);
    partial void OnUserIDChanged();
    partial void OnGroupIdChanging(int value);
    partial void OnGroupIdChanged();
    partial void OnAddDateChanging(System.DateTime value);
    partial void OnAddDateChanged();
    #endregion
		
		public GroupUser()
		{
			this._Group = default(EntityRef<Group>);
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GroupId", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int GroupId
		{
			get
			{
				return this._GroupId;
			}
			set
			{
				if ((this._GroupId != value))
				{
					if (this._Group.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnGroupIdChanging(value);
					this.SendPropertyChanging();
					this._GroupId = value;
					this.SendPropertyChanged("GroupId");
					this.OnGroupIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AddDate", DbType="Date NOT NULL")]
		public System.DateTime AddDate
		{
			get
			{
				return this._AddDate;
			}
			set
			{
				if ((this._AddDate != value))
				{
					this.OnAddDateChanging(value);
					this.SendPropertyChanging();
					this._AddDate = value;
					this.SendPropertyChanged("AddDate");
					this.OnAddDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Group_GroupUser", Storage="_Group", ThisKey="GroupId", OtherKey="GroupID", IsForeignKey=true)]
		public Group Group
		{
			get
			{
				return this._Group.Entity;
			}
			set
			{
				Group previousValue = this._Group.Entity;
				if (((previousValue != value) 
							|| (this._Group.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Group.Entity = null;
						previousValue.GroupUsers.Remove(this);
					}
					this._Group.Entity = value;
					if ((value != null))
					{
						value.GroupUsers.Add(this);
						this._GroupId = value.GroupID;
					}
					else
					{
						this._GroupId = default(int);
					}
					this.SendPropertyChanged("Group");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_GroupUser", Storage="_User", ThisKey="UserID", OtherKey="UserID", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.GroupUsers.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.GroupUsers.Add(this);
						this._UserID = value.UserID;
					}
					else
					{
						this._UserID = default(int);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.UserPermission")]
	public partial class UserPermission : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserID;
		
		private int _PermID;
		
		private System.DateTime _AddDate;
		
		private EntityRef<Permission> _Permission;
		
		private EntityRef<User> _User;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIDChanging(int value);
    partial void OnUserIDChanged();
    partial void OnPermIDChanging(int value);
    partial void OnPermIDChanged();
    partial void OnAddDateChanging(System.DateTime value);
    partial void OnAddDateChanged();
    #endregion
		
		public UserPermission()
		{
			this._Permission = default(EntityRef<Permission>);
			this._User = default(EntityRef<User>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					if (this._User.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PermID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int PermID
		{
			get
			{
				return this._PermID;
			}
			set
			{
				if ((this._PermID != value))
				{
					if (this._Permission.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPermIDChanging(value);
					this.SendPropertyChanging();
					this._PermID = value;
					this.SendPropertyChanged("PermID");
					this.OnPermIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AddDate", DbType="Date NOT NULL")]
		public System.DateTime AddDate
		{
			get
			{
				return this._AddDate;
			}
			set
			{
				if ((this._AddDate != value))
				{
					this.OnAddDateChanging(value);
					this.SendPropertyChanging();
					this._AddDate = value;
					this.SendPropertyChanged("AddDate");
					this.OnAddDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Permission_UserPermission", Storage="_Permission", ThisKey="PermID", OtherKey="PermID", IsForeignKey=true)]
		public Permission Permission
		{
			get
			{
				return this._Permission.Entity;
			}
			set
			{
				Permission previousValue = this._Permission.Entity;
				if (((previousValue != value) 
							|| (this._Permission.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Permission.Entity = null;
						previousValue.UserPermissions.Remove(this);
					}
					this._Permission.Entity = value;
					if ((value != null))
					{
						value.UserPermissions.Add(this);
						this._PermID = value.PermID;
					}
					else
					{
						this._PermID = default(int);
					}
					this.SendPropertyChanged("Permission");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_UserPermission", Storage="_User", ThisKey="UserID", OtherKey="UserID", IsForeignKey=true, DeleteOnNull=true, DeleteRule="CASCADE")]
		public User User
		{
			get
			{
				return this._User.Entity;
			}
			set
			{
				User previousValue = this._User.Entity;
				if (((previousValue != value) 
							|| (this._User.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._User.Entity = null;
						previousValue.UserPermissions.Remove(this);
					}
					this._User.Entity = value;
					if ((value != null))
					{
						value.UserPermissions.Add(this);
						this._UserID = value.UserID;
					}
					else
					{
						this._UserID = default(int);
					}
					this.SendPropertyChanged("User");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Permissions")]
	public partial class Permission : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _PermID;
		
		private string _Name;
		
		private bool _Active;
		
		private string _DispName;
		
		private EntitySet<GroupPermission> _GroupPermissions;
		
		private EntitySet<UserPermission> _UserPermissions;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPermIDChanging(int value);
    partial void OnPermIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnActiveChanging(bool value);
    partial void OnActiveChanged();
    partial void OnDispNameChanging(string value);
    partial void OnDispNameChanged();
    #endregion
		
		public Permission()
		{
			this._GroupPermissions = new EntitySet<GroupPermission>(new Action<GroupPermission>(this.attach_GroupPermissions), new Action<GroupPermission>(this.detach_GroupPermissions));
			this._UserPermissions = new EntitySet<UserPermission>(new Action<UserPermission>(this.attach_UserPermissions), new Action<UserPermission>(this.detach_UserPermissions));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PermID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int PermID
		{
			get
			{
				return this._PermID;
			}
			set
			{
				if ((this._PermID != value))
				{
					this.OnPermIDChanging(value);
					this.SendPropertyChanging();
					this._PermID = value;
					this.SendPropertyChanged("PermID");
					this.OnPermIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Active", DbType="Bit NOT NULL")]
		public bool Active
		{
			get
			{
				return this._Active;
			}
			set
			{
				if ((this._Active != value))
				{
					this.OnActiveChanging(value);
					this.SendPropertyChanging();
					this._Active = value;
					this.SendPropertyChanged("Active");
					this.OnActiveChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DispName", DbType="VarChar(50)")]
		public string DispName
		{
			get
			{
				return this._DispName;
			}
			set
			{
				if ((this._DispName != value))
				{
					this.OnDispNameChanging(value);
					this.SendPropertyChanging();
					this._DispName = value;
					this.SendPropertyChanged("DispName");
					this.OnDispNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Permission_GroupPermission", Storage="_GroupPermissions", ThisKey="PermID", OtherKey="GroupID")]
		public EntitySet<GroupPermission> GroupPermissions
		{
			get
			{
				return this._GroupPermissions;
			}
			set
			{
				this._GroupPermissions.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Permission_UserPermission", Storage="_UserPermissions", ThisKey="PermID", OtherKey="PermID")]
		public EntitySet<UserPermission> UserPermissions
		{
			get
			{
				return this._UserPermissions;
			}
			set
			{
				this._UserPermissions.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_GroupPermissions(GroupPermission entity)
		{
			this.SendPropertyChanging();
			entity.Permission = this;
		}
		
		private void detach_GroupPermissions(GroupPermission entity)
		{
			this.SendPropertyChanging();
			entity.Permission = null;
		}
		
		private void attach_UserPermissions(UserPermission entity)
		{
			this.SendPropertyChanging();
			entity.Permission = this;
		}
		
		private void detach_UserPermissions(UserPermission entity)
		{
			this.SendPropertyChanging();
			entity.Permission = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Users")]
	public partial class User : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserID;
		
		private string _Name;
		
		private string _Pass;
		
		private bool _Active;
		
		private string _FName;
		
		private string _LName;
		
		private EntitySet<GroupUser> _GroupUsers;
		
		private EntitySet<UserPermission> _UserPermissions;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIDChanging(int value);
    partial void OnUserIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnPassChanging(string value);
    partial void OnPassChanged();
    partial void OnActiveChanging(bool value);
    partial void OnActiveChanged();
    partial void OnFNameChanging(string value);
    partial void OnFNameChanged();
    partial void OnLNameChanging(string value);
    partial void OnLNameChanged();
    #endregion
		
		public User()
		{
			this._GroupUsers = new EntitySet<GroupUser>(new Action<GroupUser>(this.attach_GroupUsers), new Action<GroupUser>(this.detach_GroupUsers));
			this._UserPermissions = new EntitySet<UserPermission>(new Action<UserPermission>(this.attach_UserPermissions), new Action<UserPermission>(this.detach_UserPermissions));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Pass", DbType="VarChar(32) NOT NULL", CanBeNull=false)]
		public string Pass
		{
			get
			{
				return this._Pass;
			}
			set
			{
				if ((this._Pass != value))
				{
					this.OnPassChanging(value);
					this.SendPropertyChanging();
					this._Pass = value;
					this.SendPropertyChanged("Pass");
					this.OnPassChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Active", DbType="Bit NOT NULL")]
		public bool Active
		{
			get
			{
				return this._Active;
			}
			set
			{
				if ((this._Active != value))
				{
					this.OnActiveChanging(value);
					this.SendPropertyChanging();
					this._Active = value;
					this.SendPropertyChanged("Active");
					this.OnActiveChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FName", DbType="VarChar(50)")]
		public string FName
		{
			get
			{
				return this._FName;
			}
			set
			{
				if ((this._FName != value))
				{
					this.OnFNameChanging(value);
					this.SendPropertyChanging();
					this._FName = value;
					this.SendPropertyChanged("FName");
					this.OnFNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LName", DbType="VarChar(50)")]
		public string LName
		{
			get
			{
				return this._LName;
			}
			set
			{
				if ((this._LName != value))
				{
					this.OnLNameChanging(value);
					this.SendPropertyChanging();
					this._LName = value;
					this.SendPropertyChanged("LName");
					this.OnLNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_GroupUser", Storage="_GroupUsers", ThisKey="UserID", OtherKey="UserID")]
		public EntitySet<GroupUser> GroupUsers
		{
			get
			{
				return this._GroupUsers;
			}
			set
			{
				this._GroupUsers.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="User_UserPermission", Storage="_UserPermissions", ThisKey="UserID", OtherKey="UserID")]
		public EntitySet<UserPermission> UserPermissions
		{
			get
			{
				return this._UserPermissions;
			}
			set
			{
				this._UserPermissions.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_GroupUsers(GroupUser entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_GroupUsers(GroupUser entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
		
		private void attach_UserPermissions(UserPermission entity)
		{
			this.SendPropertyChanging();
			entity.User = this;
		}
		
		private void detach_UserPermissions(UserPermission entity)
		{
			this.SendPropertyChanging();
			entity.User = null;
		}
	}
}
#pragma warning restore 1591