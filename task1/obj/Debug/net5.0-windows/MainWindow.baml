   M S B A M L   `   `   `  ����@  <task1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Task1Task1  5   	       Task1.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:Task1     5   	   -   5        $��main_window��5   �      m_Title  � Title#  �   6	   $	��800��6+   $
��1000��68   
�������.��5	      � -   5         �Name$ 	main_grid��5	       � ColumnDefinitions 5
   
   �� 5      $	��200 �6   6-   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      $	��150 �6   6+   �� 5      6   .+�5   
   �� $��Center=�6    �� Orientation$ Vertical=�62   $��Center=�6I   $��0��6e   $��0��6r   .+�5      �� -   5         ��Name$ btnExit��5      
���   �$	��1pxq�6:   $
��30px��6P   $
��10pxq�6^   $��Center=�6l   $��Center=�6�   
�������  	m_btnExit#���  6�   6�   o� 5      
���   �$	��1pxq�6'   $
��30px��6=   $
��10pxq�6K   $��Center=�6Y   $��Center=�6t   
��� ���.@�5      m� -   5         m�Name$ itemFile��5   )   $
��30px��6     	m_btnFile#���  6;   .@�5      m� -   5        $ itemSave��5   -   $
��30px��6     	m_btnSave#���  6?   .@�5      m� -   5        $ 
btnSaveBin��5   1   $
��30px��6#     m_btnSaveBin#���  6^   m� 5      -   5        $ btnSaveJSON��5   1   $
��30px��6#     m_btnSaveJSON#���  6`   m� 5      -   5        $ 
btnSaveXML��5   1   $
��30px��6#     m_btnSaveXML#���  6^   5      m� 5      -	   5        $ itemLoad��5   -   $
��30px��6     	m_btnLoad#���  6?   .@�5      m� -
   5        $ 
btnLoadBin��5   1   $
��30px��6#     m_btnLoadBin#���  6^   m� 5      -   5        $ btnLoadJSON��5   1   $
��30px��6#    	 m_btnLoadJSON#��� 	 6`   m� 5      -   5        $ 
btnLoadXML��5   1   $
��30px��6#    
 m_btnLoadXml#��� 
 6^   5       5!      5"      o� 5#      
���   �$	��1pxq�6'   $
��30px��6=   $
��10pxq�6K   $��Center=�6Y   $��Center=�6t   
��� ���.@�5$      m� -   5        $ menuLanguage��5$   )   $
��30px��6     m_menu_Language#���  6?   5%      5&      �� 5'   
   $��Center=�6   $ 
Horizontal=�62   $��0��6K   $��1��6X   .+�5(      �� -   5        $��addTriangleBtn��5(   |   
���   �$	��1pxq�6)   
��� ���$
��90px��6S   $
��50px��6`   $
��20pxq�6n     m_btnTriangle#���  6�   6�   �� 5)      -   5        $��addCircleBtn��5)   |   
���   �$	��1pxq�6)   
��� ���$
��90px��6S   $
��50px��6`   $
��20pxq�6n     m_btnCircle#���  6�   6�   �� 5*      -   5        $��addRectangleBtn��5*   |   
���   �$	��1pxq�6)   
��� ���$
��90px��6S   $
��50px��6`   $
��20pxq�6n     m_btnRectangle#���  6�   6�   5+      �� 5,   
   $��Center=�6   $ 
Horizontal=�62   $��0��6K   $��2��6X   .+�5-      �� -   5        $��startBtn��5-   �   
���   �$	��1pxq�6)   
������$��100px��6W   $
��50px��6e   $
��20pxq�6s     
m_btnStart#���  6�   6�   �� 5.      -   5        $��stopBtn��5.   �   
���   �$	��1pxq�6)   
������$��100px��6W   $
��50px��6e   $
��20pxq�6s     	m_btnStop#���  6�   6�   5/      R� 50   
   -   5        $��0q�50   F   
�������$��1��6n   $��0��6{   .@�51      Q� -   5        $��treeViewItemFigures��51   A    Q� 
IsExpanded$
 True��6   
��� ���  m_treeViewFigures#���  6\   .@�52      Q� -   5        $��treeViewItemCircle��52   E   
��� ���$
 True��63     m_treeViewItemCircle#���  6_   6�   Q� 53      -   5        $��treeViewItemRectangle��53   E   
��� ���$
 True��63     m_treeViewItemRectangle#���  6b   6�   Q� 54      -   5        $��treeViewItemTriangle��54   E   
��� ���$
 True��63     m_treeViewItemTriangle#���  6a   6�   55      56      �� 57   
   -   5        $��canvasFigures��57   �   
�������$
}�True��6P   $��1��6d   $��1��6q   $��2��6�   6�   58      59      