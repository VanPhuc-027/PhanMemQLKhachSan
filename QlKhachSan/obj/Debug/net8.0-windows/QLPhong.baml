   M S B A M L   `   `   `  ����  
QlKhachSan    QlKhachSan.QLPhong   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Quản Lý Phòng��5   	   $
��Auto��5   	   $
��Auto��6    � WindowState$ 	Maximized=�5   	   ���	#FFC8DDD4.��5	      � ���	#FFC8DDD4$��10q�6"    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ]� $��0��6   $��False��6!   $��0,0,0,10q�67   .+�5      �� $��Quản Lý Phòng��6   $��30(�6-   $
��Bold"�6;   
�������$
��10,5q�5   +   $��Center=�5      $
��Left=�5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ Black��6'      	Direction$	 320Q�65      ShadowDepth$ 3Q�6E      
BlurRadius$ 5Q�6U   ��5      �� 5       �� 
StartPoint$	 0,0&�6*   	 �� EndPoint$		 1,1&�6;   .��5      � 
 � Color$
 #F27C50��6'    � Offset$ 0Q�67   � 5      $
 #F9D776��6'   $ 1Q�67   5       �� Padding$
 10,5q�5   <   5       �� 5#       �� Orientation$ 
Horizontal=�6   $��Right=�62   $��Center=�6I   .+�5$      �� -   5         ��Name$ btn_Add��5$      $��➕ Thêm Phòng��6*   $	��150��6A   $��40��6M   ���#0078D7
�������$��16(�5%   A   $
��Bold"�6O   $	��5,0q�5&       � Cursor$
 Hand��6&    5'        5(      $ Black��6+   $	 320Q�69   $ 2Q�6I   $ 5Q�6Y   5*      �� 5,      -   5        $ btn_ManageRoomType��5,      $&�� 🏷️ Quản lý hạng phòng��65   $	��180��6V   $��40��6b   ���#28A745
�������$��16(�5-   A   $
��Bold"�6O   $	��5,0q�5.      $
 Hand��6&    5/        50      $ Black��6+   $	 320Q�69   $ 2Q�6I   $ 5Q�6Y   52      �� 54      -   5        $ btn_BackToMain��54      $"��🔙 Quay lại trang chính��61   $	��200��6S   $��40��6_   ���#DC3545
�������$��16(�55   A   $
��Bold"�6O   $	��5,0q�56      $
 Hand��6&    57        58      $ Black��6+   $	 320Q�69   $ 2Q�6I   $ 5Q�6Y   5:      5;      5<      &   System.Windows.Controls.DataGrid  5?   
   -   5        $��dgDanhSachPhong��5?         AutoGenerateColumns$ False��6*      CanUserAddRows$ False��5@         
IsReadOnly$
 True��6*   
�������
���   �
�������   	RowHeight$ 40Q�5B         HeadersVisibility$ Column=�6"   $��10q�5C      $��5q�6   $��1��5D      $��Center=�5E      $��Center=�5F      $	��800��5G         AlternatingRowBackground �#F9F9F9   SelectionMode$ Single=�5I         SelectionUnit$ FullRow=�5J         Columns 5L      0  *System.Windows.Controls.DataGridTextColumn  5M      ,  &System.Windows.Controls.DataGridColumn   Header$ Số Phòng��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *
RoomNumber+   ElementStyle 5N      �� 5O       �� 
TargetType" ��6    .�5P      ��  �� Property � �� �� Value$ Center=�6D   �� 5Q       � ��$ Center=�6B   �� 5R       �@��TextAlignment$ Center=�6>   �� 5S       � ��$
 Bold"�6;   5T      5V        5X      $ Loại Phòng��6%   $ * 6V    69   �� *
TypeName+ 5Y      �� 5Z      " ��6    .�5[      ��  � ��$ Center=�6D   �� 5\       � ��$ Center=�6B   �� 5]       �@��TextAlignment$ Center=�6>   �� 5^       � ��$
 Bold"�6;   5_      5a        5c      $ Giá theo giờ��6%   $ * 6Y    6;   �� *	PriceHour+ 5d      �� 5e      " ��6    .�5f      ��  � ��$ Center=�6D   �� 5g       � ��$ Center=�6B   �� 5h       �@��TextAlignment$ Center=�6>   5i      5k        5m      $ Giá theo ngày��6%   $ * 6Y    6<   �� *
PriceDay+ 5n      �� 5o      " ��6    .�5p      ��  � ��$ Center=�6D   �� 5q       � ��$ Center=�6B   �� 5r       �@��TextAlignment$ Center=�6>   5s      5u      4  .System.Windows.Controls.DataGridTemplateColumn  5w      $ 	Tác vụ��6)   $	 100 69       CellTemplate  5x      �� 5y      .R�5z      �� $ 
Horizontal=�6)   .+�5{   "   �� -   5        $��✏️��5{   )   $��0,0,5,0q�6K   $��30��6\   ���#FFC107
�������
������  5}   &     5~   *   $ Black��6;   $	 320Q�6I   $ 2Q�6Y   $ 5Q�6i   5�   #   �� 5�   "   -   5        $��🗑️��5�   )   $��30��6N   ���#DC3545
�������
������  5�   &     5�   *   $ Black��6;   $	 320Q�6I   $ 2Q�6Y   $ 5Q�6i   5�   #   5�      5�      5�      5�      5�      5�      