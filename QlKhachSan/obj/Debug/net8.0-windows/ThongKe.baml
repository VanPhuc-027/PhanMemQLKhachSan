   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5   	    
QlKhachSan   QlKhachSan.ThongKe   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	     � Title$'  !Thống kê doanh thu theo tháng��5   	   $	��450��6/   $	��800��6<    � WindowState$ 	Maximized=�5   	   .��5      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� ���	#FFC8DDD4$��0��6(    �� Padding$ 10q�65   $��0,0,0,10q�6B    �� CornerRadius$ 10��6T   ._�5      �� $,��&📊 Thống kê doanh thu theo tháng��6   $��28(�5      $
��Bold"�6&   
���   �$��Center=�5      $��Center=�65    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$
 Gray��6'      	Direction$	 320Q�64      ShadowDepth$ 2Q�6D   	   
BlurRadius$	 4Q�6T   5      5      � 5   
   $��1��6   $��0,0,0,10q�6   
 � ColumnDefinitions
 5       �� 5!      $��* �6#   �� 5"      $
��Auto �6#   .+�5&      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5'      �� -   5         ��Name$ cbThang��5'      $	��120��6,   $��5q�68   6C   �� 5(      -   5        $ cbNam��5(      $	��120��6*   $��5q�66   6A   5)      �� 5,      -   5         ��Name$ btn_BackToMain��5,      $"��🔙 Quay lại trang chính��6-   $	��200��6O   $��40��6[   ���#DC3545
�������$��16(�5-   =   $
��Bold"�6K   $��10,0,0,0q�5.       � Cursor$
 Hand��6'   $��1��5/      $��Right=�6%    50        51      $ Black��6'   $	 320Q�65   $ 2Q�6E   $	 5Q�6U   53      54      �� 57   
   -   5         ��Name$ 
tbThongBao��57      $9��3Không có dữ liệu doanh thu trong tháng này.��58      
�������$��Italic$�5:      $��16(�5;      $��Center=�5<      $	��Top=�5=      $��0,0,0,10q�5>      $y�	Collapsed=�5?      $��2��5@      #   LiveCharts.Wpf.CartesianChart  5C   
   -   5        $��RevenueChart��5C      $��2��61   $��10q�6>   &    LiveCharts.Wpf.Charts.Base.Chart   AxisX 5E         LiveCharts.Wpf.Axis     Title$ Ngày��6      Labels 6(   �� *Labels+   AxisY 5H        $ Doanh thu (VNĐ)��6   5J      5K      5L      