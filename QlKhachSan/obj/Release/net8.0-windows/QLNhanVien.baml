   M S B A M L   `   `   `  ����   
QlKhachSan    QlKhachSan.QLNhanVien   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Quản lý nhân viên��$	��600��$
��1000��.���  � RowDefinitions �� $
��Auto ��� $��* �.+��  � ColumnDefinitions �� $��* ��� $
��Auto �.+���  �� Orientation$ 
Horizontal=�$��10q�.+��� -   $��Thêm��$��80��$��30��$��5q����	#FF60EE4D�� -   $��Sửa��$��80��$��30��$��5q����	#FFFFD700�� -   $
��Xóa��$��80��$��30��$��5q����	#FFFF4500�� $ 
Horizontal=�$��10q�$��1��$��Right=�.+��� -   $��	txtSearch��$	��180��$��30��$��5q�$��Tìm kiếm...��
��������� -   $
��🔍��$��40��$��30��$��5q����	#FF1E90FF&   System.Windows.Controls.DataGrid  -   $��
dgNhanVien��   AutoGenerateColumns$ False��$��Stretch=�$��Stretch=�$��10q�$��1��   CanUserResizeColumns$ False��   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ *    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��	   Width$	 50 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *
EmployeeId+  $ User ID��$	 80 
 �� *UserId+  $ 	Họ tên��   MinWidth$	 200Q�
 �� *
FullName+  $ Chức vụ��$		 150 
 �� *
Position+  $ Điện thoại��$		 120 
 �� *Phone+  $ Email��$		 200 
 �� *Email+  $ Địa chỉ��$		 250 
 �� *	Address+  $ Ngày vào làm��$	 200Q�
 �� *
HireDate+ �� StringFormat$ d��