   M S B A M L   `   `   `  ����G  CGood Teacher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Good_Teacher.Windows.SpecialGood_Teacher.Windows.Special  5   	   ;"clr-namespace:Good_Teacher.StringsGood_Teacher.Strings  5   	   =#clr-namespace:Good_Teacher.ControlsGood_Teacher.Controls  5   	   6    0Good_Teacher.Windows.Special.Window_PrintPreview   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:Good_Teacher.Windows.Special     5   	   6strings"clr-namespace:Good_Teacher.Strings     5   	   8controls#clr-namespace:Good_Teacher.Controls     5   	   %   Good_Teacher.Strings.ResStrings    PrintPreview � Title# Z  5
   	   $	��500��6<   $	��800��6L   $	��600��6[   $	��800��6h    � WindowStartupLocation$ CenterOwner=�6t    � Icon$6 0/Good Teacher;component/Resources/Menu/Print.png��6�   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��0��6   ���	#FFECF0F1.+�5      �� $��	10,10,0,8q�6   $
��Left=�6,    �� Orientation$ 
Horizontal=�6G   .+�5      �� $ Vertical=�6   $��Center=�64   .+�5      �� -   5        $��42��5      $��42��6(   .��5      �� $6��0/Good Teacher;component/Resources/Menu/Print.png��6    5      �� 5         Print#��Z 6   $��Center=�6J   5      �� 5      $	��5,0q�6   $��Center=�6*   .+�5      �� $ 
Horizontal=�6!   .+�5      (   "Good_Teacher.Controls.SelectButton  -   5          Name$ SB_Real��5   0   (   "Good_Teacher.Controls.SelectButton   OnCheckChanged$ SB_Real_OnCheckChanged��6A   $��28��6i   $��28��6t   $��10,3,3,3q�6�   	   Image_Source$>	 8/Good Teacher;component/Resources/Icons/Special/Real.png��6�   
   Hover
 �	#FFFFC1D7   	OnChecked �	#FFFD79A86�   �� 5          RealPage#��Z 6    $��Center=�6Q   5!      �� 5#      $ 
Horizontal=�6!   .+�5$        -   5        $ SB_Fit��5$   0   $ SB_Fit_OnCheckChanged��6@   $��28��6g   $��28��6r   $��10,3,3,3q�6~   $A	 ;/Good Teacher;component/Resources/Icons/Special/Stretch.png��6�    �	#FFFD79A8
 �	#FFFFC1D76�   �� 5%         FitPage#��Z 6    $��Center=�6P   5&      5(      �� 5*      $
��10,0q�6   .+�5+      �� .+�5,      ��    Color#��Z 6$   $
��Bold"�6O   �� 5-      -   5         ��Name$ CB_OutPutColor��5-   #   $	��100��6;   $��22��6G   .@�5.      ��    PrinterSettings#��Z 6+    �� 
IsSelected . 6c   �� 5/      #��Z 6+   �� 50         	Grayscale#��Z 6+   �� 51         
Monochrome#��Z 6+   52      53      �� 55      .+�56      �� $��0,5,0,0q�6$      PageOrientation#��Z 65   $
��Bold"�6j   �� 57      -   5        $ CB_Orientation��57   #   $	��100��6;   $��22��6G   .@�58      �� #��Z 6+    . 6c   �� 59         	Landscape#��Z 6+   �� 5:         Portrait#��Z 6+   �� 5;         ReverseLandscape#��Z 6+   �� 5<         ReversePortrait#��Z 6+   5=      5>      5?      �� 5A      $��10,0,0,0q�6   $��2��6.   
���   �68   �� 5C      $	��5,0q�6   $��Center=�6*   .+�5E      ��    Preview#��Z 6    $
��Bold"�6M   $��Center=�6_   �� 5G      $ 
Horizontal=�6!   .+�5H        -   5        $ 
SB_OnePage��5H   0   $ SB_OnePage_OnCheckChanged��6D   $��24��6o   $��24��6z   $��10,3,3,0q�6�   $=	 7/Good Teacher;component/Resources/Icons/ShowOnePage.png��6�   
 �	#FFFFC1D7 �	#FFFD79A86�   �� 5I         OnePage#��Z 6$   $��Center=�6Q   5J      �� 5L      $ 
Horizontal=�6!   .+�5M        -   5        $ 
SB_AllPage��5M   0   $ SB_AllPage_OnCheckChanged��6D   $��24��6o   $��24��6z   $��10,3,3,0q�6�   $;	 5/Good Teacher;component/Resources/Icons/ShowPages.png��6�    �	#FFFD79A8
 �	#FFFFC1D76�   �� 5N         AllPages#��Z 6$   $��Center=�6R   5O      5Q      �� 5U      $	��5,0q�6   $��Center=�6*   .+�5W      ��    View#��Z 6    $
��Bold"�6J   $��Center=�6\   �� 5Y      $ 
Horizontal=�6!   .+�5Z        -   5        $ SB_ScaleView��5Z   0   $! SB_ScaleView_OnCheckChanged��6F   $��24��6s   $��24��6~   $��10,3,3,0q�6�   $;	 5/Good Teacher;component/Resources/Icons/ScalePage.png��6�   
 �	#FFFFC1D7 �	#FFFD79A86�   �� 5[         Adjust#��Z 6$   $��Center=�6P   5\      �� 5^      $ 
Horizontal=�6!   .+�5_        -	   5        $ SB_RealView��5_   0   $  SB_RealView_OnCheckChanged��6E   $��24��6q   $��24��6|   $��10,3,3,0q�6�   $:	 4/Good Teacher;component/Resources/Icons/RealPage.png��6�    �	#FFFD79A8
 �	#FFFFC1D76�   �� 5`         RealView#��Z 6$   $��Center=�6R   5a      5c      �� 5e      $ Vertical=�6   $��10,0,0,0q�64   $��Center=�6F   .+�5f      �� -
   5        $��32��5f      $��32��6(   .��5g      �� $��4q�6    $B��</Good Teacher;component/Resources/Icons/Buttons/Previous.png��6+   5h      �� 5i         Previous#��Z 6   $��Center=�6M   5j      �� 5m      $ Vertical=�6   $��10,0,0,0q�64   $��Center=�6F   .+�5n      �� -   5        $��32��5n      $��32��6(   .��5o      �� $��4q�6    $>��8/Good Teacher;component/Resources/Icons/Buttons/Next.png��6+   5p      �� 5q          Next#��Z  6   $��Center=�6I   5r      �� 5t      $��Center=�6   $
��10,0q�68   .+�5u      �� !   Page#��Z! 6   $��Center=�6I   $
��Bold"�6d   �� 5v      -   5        " ��Name$" L_Page��5v      $	��0/0��6,   $��14(�6:   $��Center=�6H   5w      5y      5{      �� 5}   
   -   5        # ��Name$# 
SV_Preview��5}      $
��Auto=�6+   $
��Auto=�6P   $��1��6s   .��5      (� -   5        $ (�Name$$ VB_Image��5      .��5�      �� -   5        % ��Name$% SPanel��5�      $ Vertical=�6-   5�      5�      5�      5�      5�      