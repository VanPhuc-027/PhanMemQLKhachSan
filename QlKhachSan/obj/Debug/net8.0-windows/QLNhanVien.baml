   M S B A M L   `   `   `  ����  
QlKhachSan    QlKhachSan.QLNhanVien   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Quản lý nhân viên��5   	   $	��600��6#   $
��1000��60    � WindowState$ 	Maximized=�5   	   .��5      �  � RowDefinitions 5      �� 5	      $
��Auto �6   �� 5
      $��* �6   .+�5      �  � ColumnDefinitions 5      �� 5      $��* �6   �� 5      $
��Auto �6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��10q�6)   .+�5      �� -   5        $��Thêm��5      $��80��6   $��30��6'   $��5q�63   ���	#FF60EE4D6U   �� 5      -   5        $��Sửa��5      $��80��6   $��30��6&   $��5q�62   ���	#FFFFD7006T   �� 5      -   5        $
��Xóa��5      $��80��6   $��30��6&   $��5q�62   ���	#FFFF45006T   5      �� 5      $ 
Horizontal=�6   $��10q�6)   $��1��65   $��Right=�6E   .+�5      �� -   5        $��	txtSearch��5      $	��180��6   $��30��6+   $��5q�67   $��Tìm kiếm...��6B   
�������5    8   �� 5!      -   5        $
��🔍��5!      $��40��6   $��30��6%   $��5q�61   ���	#FF1E90FF6S   5"      5#      &   System.Windows.Controls.DataGrid  5%   
   -   5        $��
dgNhanVien��5%         AutoGenerateColumns$ False��6%   $��Stretch=�5&      $��Stretch=�61   $��10q�5'      $��1��6      CanUserResizeColumns$ False��5(         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5)         Columns 5*      0  *System.Windows.Controls.DataGridTextColumn  5+      ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��6!   
   Width$
 50 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6-   �� *
EmployeeId+6L     5,      $	 User ID��6!   $
 80 6M    62   �� *UserId+6M     5-      $	 	Họ tên��6!      MinWidth$	 200Q�6N    61   �� *
FullName+6N     5.      $	 Chức vụ��6!   $	
 150 6O    62   �� *
Position+6O     5/      $	 Điện thoại��6!   $	
 120 6O    65   �� *Phone+6O     50      $	 Email��6!   $	
 200 6J    60   �� *Email+6J     51      $	 Địa chỉ��6!   $	
 250 6N    62   �� *	Address+6N     52      $	 Ngày vào làm��6!   $	 200Q�6d    67   �� *
HireDate+ �� StringFormat$ d��6d   54      55      56      