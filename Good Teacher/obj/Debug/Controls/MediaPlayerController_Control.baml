   M S B A M L   `   `   `  ����G  CGood Teacher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Good_Teacher.ControlsGood_Teacher.Controls  5      M+clr-namespace:Good_Teacher.Controls.SpecialGood_Teacher.Controls.Special  5      9    3Good_Teacher.Controls.MediaPlayerController_Control   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      5local#clr-namespace:Good_Teacher.Controls     5      ?Special+clr-namespace:Good_Teacher.Controls.Special     5      -   5          >�Name$  mediaplayercontroller_C��5   J   $	��400��5	   7   $	��250��6F   
���   �$��1q�6j   
���   �.��5      �  � RowDefinitions 5   
   �� 5      �� 5      $
��Auto �6   .+�5   
   r� -   5         r�Name$ ME_MediaPlayer��5      $��0��6/    r� LoadedBehavior$ Manual=�6<   6�   �� 5   
   -   5        $��1��5      ��69   ��  �� ElementName$ mediaplayercontroller_C�� �� Path$ ControlPanelBackground�._�5      � -   5         �Name$ G_ControlPanel��5       � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      �� 5      $
��Auto �6'   .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�66   $��0��6Q   .+�5      � -   5        	 �Name$	 PB_TimePlay��5   "   $
��10,0q�6m   $	��100��6{   $��8��6�   
 � Value$
 0Q�6�   ���	#FF2980B9 � Cursor$
 Hand��6�   �� 5      -   5         ��Name$ LB_CTime��5       $
��0:00��62   $��Center=�6>   �� 5      $	��5,0q�6    $��/��6-   $��Center=�66   �� 5       -   5        $ LB_Time��5        $
��0:00��61   $��Center=�6=   5!      �� 5#      $ 
Horizontal=�6   $��Center=�66   $��1��6S   .+�5$      &    Good_Teacher.Controls.FlatButton  &    Good_Teacher.Controls.FlatButton   Click$# FlatButtonPlayFromStart_Click��6>   $��24��6'   $��24��62   $	��5,2q�6d      Image_Source$E ?/Good Teacher;component/Resources/Icons/Media/PlayFromStart.png��6q      Hover �	#7FFFFFFF   
ClickBrush �	#CCFFFFFF6�     5%      -   5          Name$ FB_PlayPause��5%   '   $ FlatButtonPlayPause_Click��6=   $��24��6_   $��24��6j   $	��5,2q�6v   $< 6/Good Teacher;component/Resources/Icons/Media/Play.png��6�    �	#7FFFFFFF �	#CCFFFFFF6�   5&      �� 5(      $ 
Horizontal=�6   $��Right=�66   $��2��6R   .+�5)      �� $��20��6   $��20��6'   $>��8/Good Teacher;component/Resources/Icons/Media/Volume.png��63   �� 5*      -	   5         ��Name$ 	SL_Volume��5*      $
��10,0q�60   $��Center=�6>    �� AutoToolTipPlacement$ TopLeft=�6Y   $��70��6x    � Maximum$	 100Q�6�    � SmallChange$ 1Q�6�   $
 50Q�6�    �� TickPlacement$ TopLeft=�6�    �� TickFrequency$ 25Q�6�   5+      5-      5/      51      52      