   M S B A M L   `   `   `  ����   
QlKhachSan    QlKhachSan.QLPhong   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     $
��Auto��$
��Auto��.��� ���#F4F4F4$��10q�  � RowDefinitions  �� $
��Auto ��� $��* �.+��  � ColumnDefinitions �� $
��Auto ��� $��* �.+��� $��➕ Thêm Phòng��$	��150��$��40�����#0078D7
�������$��16(�$
��Bold"�$��10q�$
��Left=� � Cursor$
 Hand���� $��Quản Lý Phòng��$��24(�$
��Bold"����#333$��Center=�$��Center=�$��1��&   System.Windows.Controls.DataGrid  -   $��dgDanhSachPhong��   AutoGenerateColumns$ False��   CanUserAddRows$ False��   
IsReadOnly$
 True��
�������
���   �
�������   	RowHeight$ 40Q�   HeadersVisibility$ Column=�$��10q�$��5q�$��1��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 Số Phòng��
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 100 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	SoPhong+  $	 Loại Phòng��$	
 200  �� *	LoaiPhong+  $	 Giá (VNĐ)��$	
 150  �� *Gia+  $	 	Khu Vực��$	
 150  �� *KhuVuc+