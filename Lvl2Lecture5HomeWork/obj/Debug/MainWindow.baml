   M S B A M L   `   `   `  ����O  KLvl2Lecture5HomeWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Lvl2Lecture5HomeWorkLvl2Lecture5HomeWork  5   	   %    Lvl2Lecture5HomeWork.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:Lvl2Lecture5HomeWork     5   	     � Title$)  #Список сотрудников��5   	   $	��350��6$   $	��525��61   .��5	      �  � RowDefinitions 5
   
   �� 5      $��20 �6   �� 5      .+�5   
   � $��0��6   .+�5      o� .@�5      m� -   5        $��20��5      $	��220��6'   $F��@Редактировать профиль сотрудников��63   6}   m� 5      -   5        $��20��5      $	��200��6'   $>��8Добавить/удалить департаменты��63   6x   m� 5      -   5        $��20��5      $��50��6'   $��
Выход��62   6]   5      5      � 5      $��1��6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!    5   
   �� 5      $��50 �6   6*   �� 5      6   .+�5   
   �� $��0��6   $��0��6    $!��ФИО сотрудника��6-   $	��Top=�6F   $��Center=�6^   $��20(�6{   $
��Bold"�6�   6�   �� 5    
   $��1��6   $��0��6    $��Департамент��6-   $	��Top=�6C   $��Center=�6[   $
��Bold"�6x   $��20(�6�   6�   �� 5!      -   5        $��lbEmployees��5!      $��0��6*   $��1��6:   ��6G   ��  �� ElementName$ lbEmployees�� �� View 5"      �� 5#      .P�5$      ��  �� Width$
 Auto��6Y    �� DisplayMemberBinding 6)   ��  �� Path$ 	FirstName�6h   �� 5%      $
 Auto��6X    6)   �� $ LastName�6g   5&      5(      �� 5)      -   5        $��lbDepartment��5)      $��1��6+   $��1��6;   ��6H   �� $ lbEmployees�� �� Mode$ OneWay=� 5*      �� 5+      .P�5,      �� $
 Auto��6i    6)   �� $ Department.NameDepartment�6y   5-      5/      50      51      52      