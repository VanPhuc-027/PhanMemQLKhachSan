   M S B A M L   `   `   `  ����   
QlKhachSan    QlKhachSan.QLHangHoa   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Quản lý hàng hóa��$	��400��$	��600��.���  � RowDefinitions �� $
��Auto ��� $��* �.+��  � ColumnDefinitions �� $��* ��� $
��Auto �.+���  �� Orientation$ 
Horizontal=�$��10q�.+��� -   $��Thêm��$��80��$��30��$��5q����	#FF60EE4D�� -   $��Sửa��$��80��$��30��$��5q����	#FFFFD700�� -   $
��Xóa��$��80��$��30��$��5q����	#FFFF4500�� $ 
Horizontal=�$��10q�$��1��$��Right=�.+��� -   $��	txtSearch��$	��180��$��30��$��5q�$��Tìm kiếm...��
��������� -   $
��🔍��$��40��$��30��$��5q����	#FF1E90FF&   System.Windows.Controls.DataGrid  -   $��dgInventory��$��1��$��10q�   AutoGenerateColumns$ False��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	ServiceId+  $ 
Tên hàng��$	 200  �� *ServiceName+  $
 Giá��$	 100  �� *ServicePrice+  $ Mô tả��$	 250  �� *Description+