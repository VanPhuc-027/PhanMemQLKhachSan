   M S B A M L   `   `   `  ����  
QlKhachSan    QlKhachSan.EditNhanVien   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Cập nhật nhân viên��5   	   $	��400��6$   $	��500��61   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� .+�5      �� $��
Họ tên:��6   $
��Bold"�6'   �� 5      -   5        $��txtFullName��5      �� 5      $��Chức vụ:��6   $
��Bold"�6(   �� 5      -   5        $��txtPosition��5      �� 5      $��Điện thoại:��6   $
��Bold"�6+   �� 5      -   5        $��txtPhone��5      �� 5      $��Email:��6   $
��Bold"�6&   �� 5      -   5        $��txtEmail��5      �� 5      $��Chọn tài khoản:��6   $
��Bold"�6/   �� 5      -   5        $��cmbUsers��5       �� DisplayMemberPath$ Username��6'    �� SelectedValuePath$ UserId��6D   �� 5      $��Địa chỉ:��6   $
��Bold"�6(   �� 5      -   5        $��
txtAddress��5      �� 5      $��Ngày vào làm:��6   $
��Bold"�6-   (  "System.Windows.Controls.DatePicker  5      -   5        $��
dpHireDate��5      5       �� 5"   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��10q�6W   .+�5#      �� -   5        $
��Lưu��5#      $��80��6#   $��5q�6.   69   �� 5$      -	   5        $��Hủy��5$      $��80��6#   $��5q�6.   69   5%      5&      5'      