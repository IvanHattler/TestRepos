   M S B A M L   `   `   `  ����?  ;Lab4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Lab4.ViewModelsLab4.ViewModels  5   	       Lab4.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Lab4.ViewModels     5   	      grayWindowStyle#���   5   	     � Icon$  	Icon1.ico��5       � WindowStartupLocation$ CenterScreen=�6+    � Title$ 
MainWindow��5	   	   $	��455��6   $	��700��6)    � DataContext 5
      #  Lab4.ViewModels.MainViewModel  5   
   6    .��5      � 
�����_� � RowDefinitions 5   
   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   
�������.+�5      �� $
��Load��6   $��3q�6B   ��6$   ��  �� Path$
 Load�6O   �� 5      	 �� X1$ 0��6   	 �� Y1$ 0��6   		 �� X2$	 0��6!   	
 �� Y2$
 35��6(   
���   �$��Center=�6?   $��3,0,3,0q�6Z   6m   �� 5      $��Center=�6   .��6/   
Transform:6;   �� 5      $
��auto��6r   $
��auto��6   $��3q�6�   ��6   �� $ ListTransform� �� SelectedItem 6B   �� $ SelectedTransform���5      �� 5      .R�5      � .+�5      �� $
��auto��6(   $
��auto��65   ���	#FF9E63395      A� 5      
���   �$��3��63   $��3��6=   $
��Left=�6H   $��5,0,0,0q�6c   6v   �� 5      $��Arial)�68   $
��auto��6K   $
��auto��6X   $��7,0,10,0q�6f   ��6$   �� 6z   5      5      5!      �� 5"      $��Apply��6   $��3q�6D   ��6%   �� $ Apply�6Q   �� 5#      $��Reload��6   $��3q�6F   $��58��6Q   ��6&   �� $ Reload�6^   �� 5$      $��WriteMemory��6   $	��0,3q�6P   $��88��6]   ��6+   �� $ WriteMemory�6j   �� 5%      $
��Save��6   
�������$��Right=�6a   $��3q�6}   $��45��6�   ��6$   �� *Save+��6P   Z�  � ContextMenu 5&      �� 5'      .@�5(      m� $"��Open folder in File Explorer��6#    m� Command 6I   �� $ 
OpenFolder�6o   5)      5+      5,      �� 5-   
   $��1��6   $	��Top=�6!   
���   �$��0,-1,0,0q�6L   6`   �� 5.   
   $��1��6   ��6   �� $ Wb�6:   5/      50      