   M S B A M L   `   `   `  ���� ?  ;Lab4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Lab4.ViewModelsLab4.ViewModels      Lab4.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:Lab4.ViewModels        grayWindowStyle#���     � Icon$  	Icon1.ico�� � WindowStartupLocation$ CenterScreen=� � Title$ 
MainWindow��$	��455��$	��700�� � DataContext #  Lab4.ViewModels.MainViewModel  .��� 
�����_� � RowDefinitions �� $
��auto ��� .+���  �� Orientation$ 
Horizontal=�
�������.+��� $
��Load��$��3q�����  �� Path$
 Load��� 	 �� X1$ 0��	 �� Y1$ 0��		 �� X2$	 0��	
 �� Y2$
 35��
���   �$��Center=�$��3,0,3,0q��� $��Center=�.��
Transform:�� $
��auto��$
��auto��$��3q����� $ ListTransform� �� SelectedItem �� $ SelectedTransform����� .R�� .+��� $
��auto��$
��auto�����	#FF9E6339A� 
���   �$��3��$��3��$
��Left=�$��5,0,0,0q��� $��Arial)�$
��auto��$
��auto��$��7,0,10,0q����� �� $��Apply��$��3q����� $ Apply��� $��Reload��$��3q�$��58������ $ Reload��� $��WriteMemory��$	��0,3q�$��88������ $ WriteMemory��� $
��Save��
�������$��Right=�$��3q�$��45������ *Save+��Z�  � ContextMenu �� .@�m� $"��Open folder in File Explorer�� m� Command �� $ 
OpenFolder��� $��1��$	��Top=�
���   �$��0,-1,0,0q��� $��1������ $ Wb�