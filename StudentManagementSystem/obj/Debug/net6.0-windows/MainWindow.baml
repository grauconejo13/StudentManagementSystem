   M S B A M L   `   `   `  ����R  NStudentManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:StudentManagementSystemStudentManagementSystem  5   	   (    "StudentManagementSystem.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:StudentManagementSystem     5   	   
�������  � Title$  Student Management System��5   	   $	��385��6+   $	��870��68   .��5	      � .+�5   
   �� $��Student ID:��6   $
��Left=�6&   $	��Top=�6A   $��10q�6Y   �� 5   
   -   5         ��Name$ txtStudentId��5      $
��Left=�6m   $	��Top=�6�   $��100,10,10,0q�6�   $	��150��6�   ��6(   �� *SelectedItem.StudentId+ �� ElementName$ viewStudentData��6�   �� 5   
   $��Name:��6   $
��Left=�6    $	��Top=�6;   $��	10,40,0,0q�6S   �� 5   
   -   5        $ txtName��5      $
��Left=�6c   $	��Top=�6~   $��100,40,10,0q�6�   $	��150��6�   ��6#   �� *SelectedItem.Name+$ viewStudentData��6�   �� 5   
   $��Address:��6   $
��Left=�6#   $	��Top=�6>   $��	10,70,0,0q�6V   �� 5   
   -   5        $ 
txtAddress��5      $
��Left=�6i   $	��Top=�6�   $��100,70,10,0q�6�   $	��150��6�   ��6&   �� *SelectedItem.Address+$ viewStudentData��6�   �� 5   
   $��Program:��6   $
��Left=�6#   $	��Top=�6>   $��
10,100,0,0q�6V   �� 5   
   -   5        $ 
txtProgram��5      $
��Left=�6i   $	��Top=�6�   $��100,100,10,0q�6�   $	��150��6�   ��6&   �� *SelectedItem.Program+$ viewStudentData��6�   �� 5   
   $��
BirthDate:��6   $
��Left=�6%   $	��Top=�6@   $��
10,130,0,0q�6X   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dpBirthDate��5      $
��Left=�6o   $	��Top=�6�   $��100,130,10,0q�6�   $	��150��6�      Text 6*   �� *SelectedItem.BirthDate+$ viewStudentData��6�   �� 5   
   $��Total Fees:��6   $
��Left=�6&   $	��Top=�6A   $��
10,160,0,0q�6Y   �� 5   
   -   5        $ txtTotalFees��5      $
��Left=�6m   $	��Top=�6�   $��100,160,10,0q�6�   $	��150��6�   ��6(   �� *SelectedItem.TotalFees+$ viewStudentData��6�   �� 5   
   -   5        $��Insert��5      $
��Left=�6"   $	��Top=�6=   $��10,200,10,0q�6U   $��75��6j   6u   �� 5   
   -   5        $��Update��5      $
��Left=�6"   $	��Top=�6=   $��90,200,10,0q�6U   $��75��6j   6u   �� 5    
   -	   5        $��Delete��5       $
��Left=�6"   $	��Top=�6=   $��170,200,10,0q�6U   $��75��6k   6v   �� 5"   
   $��Student Management System��6   $��20(�64   $��	0,305,5,0q�6B   
����ح�6U   �� 5%   
   -
   5        $��
txtMessage��5%      $
��Left=�6&   $	��Top=�6A   $��10,270,10,0q�6Y   $
��Wrap=�6n   &   System.Windows.Controls.DataGrid  5'   
   -   5          Name$ viewStudentData��5'         AutoGenerateColumns$ False��6`   $��Right=�6|   $	��275��6�   $	��575��6�   $��
0,20,25,50q�6�   "   VerticalScrollBarVisibility$
 Auto=�5(   +      RowBackground
 �����$	��Top=�5*      	   GridLinesVisibility$		 All=�6+   ��5(      �� 
   Columns
 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Student ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 90 6d      
IsReadOnly$
 True��6o   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *	StudentId+ �� Mode$ TwoWay=�6o     5-      $
 Name��6%   $	 120 6Y   $ False��6e    63   �� *Name+$ TwoWay=�6e     5.      $ Address��6%   $	 120 6_   $ False��6k    66   �� *	Address+$ TwoWay=�6k     5/      $ Program��6%   $ * 6_   $ False��6i    66   �� *	Program+$ TwoWay=�6i     50      $ 	BirthDate��6%   $ * 6}   $
 True��6�    68   �� *	BirthDate+$ TwoWay=� �� StringFormat$ 
MM/dd/yyyy��6�     51      $ 	Total Fee��6%   $ * 6x   $ False��6�    68   �� *	TotalFees+$ TwoWay=�$ $0.00��6�   53      55      56      