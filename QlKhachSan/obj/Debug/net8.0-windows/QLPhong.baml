   M S B A M L   `   `   `  ����  
QlKhachSan    QlKhachSan.QLPhong   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Quản Lý Phòng��5   	   $
��Auto��5   	   $
��Auto��6    � WindowState$ 	Maximized=�5   	   .��5      � ���#F4F4F4$��10q�6     � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6   .+�5   
   ]� $��0��6   $��False��6!   $��0,0,0,10q�67   .+�5      �� $��Quản Lý Phòng��6   $��28(�6-   $
��Bold"�6;   
�������$
��10,5q�5   +   $��Center=�5      $
��Left=�5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ Black��6'      	Direction$	 320Q�65      ShadowDepth$ 3Q�6E      
BlurRadius$ 5Q�6U   ��5      �� 5       �� 
StartPoint$	 0,0&�6*   	 �� EndPoint$		 1,1&�6;   .��5      � 
 � Color$
 #FF6A00��6'    � Offset$ 0Q�67   � 5      $
 #FFD500��6'   $ 1Q�67   5       �� Padding$
 10,5q�5   <   5      �� 5"       �� Orientation$ 
Horizontal=�6   $��Right=�62   $��Center=�6I   .+�5#      �� -   5         ��Name$ btn_Add��5#      $��➕ Thêm Phòng��6*   $	��150��6A   $��40��6M   ���#0078D7
�������$��16(�5$   A   $
��Bold"�6O   $	��5,0q�5%       � Cursor$
 Hand��6&   64   �� 5'      -   5        $ btn_ManageRoomType��5'      $&�� 🏷️ Quản lý hạng phòng��65   $	��180��6V   $��40��6b   ���#28A745
�������$��16(�5(   A   $
��Bold"�6O   $	��5,0q�5)      $
 Hand��6&   64   �� 5+      -   5        $ btn_BackToMain��5+      $"��🔙 Quay lại trang chính��61   $	��200��6S   $��40��6_   ���#DC3545
�������$��16(�5,   A   $
��Bold"�6O   $	��5,0q�5-      $
 Hand��6&   64   5.      5/      &   System.Windows.Controls.DataGrid  52   
   -   5        $��dgDanhSachPhong��52         AutoGenerateColumns$ False��6*      CanUserAddRows$ False��53         
IsReadOnly$
 True��6"   
�������
���   �
�������   	RowHeight$ 40Q�55         HeadersVisibility$ Column=�6   $��10q�56      $��5q�6   $��1��57      $��Center=�58      $��Center=�59      $	��800��5:         Columns 5<      0  *System.Windows.Controls.DataGridTextColumn  5=      ,  &System.Windows.Controls.DataGridColumn   Header$ Số Phòng��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *
RoomNumber+   ElementStyle 5>      �� 5?       �� 
TargetType" ��6    .�5@      ��  �� Property � �� �� Value$ Center=�6D   �� 5A       � ��$ Center=�6B   �� 5B       �@��TextAlignment$ Center=�6>   5C      5E        5G      $ Loại Phòng��6%   $ * 6V    69   �� *
TypeName+ 5H      �� 5I      " ��6    .�5J      ��  � ��$ Center=�6D   �� 5K       � ��$ Center=�6B   �� 5L       �@��TextAlignment$ Center=�6>   5M      5O        5Q      $ Giá theo giờ��6%   $ * 6Y    6;   �� *	PriceHour+ 5R      �� 5S      " ��6    .�5T      ��  � ��$ Center=�6D   �� 5U       � ��$ Center=�6B   �� 5V       �@��TextAlignment$ Center=�6>   5W      5Y        5[      $ Giá theo ngày��6%   $ * 6Y    6<   �� *
PriceDay+ 5\      �� 5]      " ��6    .�5^      ��  � ��$ Center=�6D   �� 5_       � ��$ Center=�6B   �� 5`       �@��TextAlignment$ Center=�6>   5a      5c      4  .System.Windows.Controls.DataGridTemplateColumn  5e      $ 	Tác vụ��6)   $	 100 69      CellTemplate 5f      �� 5g      .R�5h      �� $ 
Horizontal=�6)   .+�5i   "   �� -   5        $��✏️��5i   )   $��0,0,5,0q�6K   $��30��6\   �� 5j   "   -   5        $��🗑️��5j   )   $��30��6N   5k      5l      5n      5q      5s      5t      