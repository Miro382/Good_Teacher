   M S B A M L   `   `   `  ���� G  CGood Teacher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Good_Teacher.ControlsGood_Teacher.Controls  -    'Good_Teacher.Controls.InkCanvas_Control   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     5local#clr-namespace:Good_Teacher.Controls     -     >�Name$  inkcanvas_C��.���� $��1q�
���   �._��  � RowDefinitions �� $��* ��� $
��Auto �.+��� -    ��Name$ 	inkCanvas��$��0��$��5��$��5���� $��1�� �� Orientation$ 
Horizontal=�����  �� ElementName$ inkcanvas_C�� �� Path$ ControlPanelBackground�.+��� -    ��Name$ Rect_BackColor��
���   �$��25��$��35��
���   �$��1��$��3q�$��Center=��� -   $��32��$��32��$��3q�.���� $��Uniform=�$B��</Good Teacher;component/Resources/EditValues/colorpicker.png���� -    ��Name$ CB_PaintSize��$��3q�$��32��$��55��$��Center=�$��Center=�.@��� $��15��$��Center=�$��Center=�.��A� $��2��$��2��
���   ��� $��15��$��Center=�$��Center=� �� 
IsSelected . .��A� $��4��$��4��
���   ��� $��15��$��Center=�$��Center=�.��A� $��8��$��8��
���   ��� $��Center=�$��Center=�.��A� $��12��$��12��
���   ��� $��Center=�$��Center=�.��A� $��15��$��15��
���   ��� $��Center=�$��Center=�.��A� $��18��$��18��
���   ��� $��Center=�$��Center=�.��A� $��24��$��24��
���   ��� $��Center=�$��Center=�.��A� $��30��$��30��
���   ��� $	��5,0q�$��2��
���   �(   "Good_Teacher.Controls.SelectButton  -   	  Name$	 Button_Draw��(   "Good_Teacher.Controls.SelectButton
   OnCheckChanged$ 
 Button_Draw_OnCheckChanged��$��32��$��32��   Image_Source$5 //Good Teacher;component/Resources/Draw/Draw.png��   	OnChecked �	#FFF06292   Hover �	#7FF06292  -   $	 Button_Eraser��$"
 Button_Eraser_OnCheckChanged��$��32��$��32��$7 1/Good Teacher;component/Resources/Draw/Eraser.png�� �	#FFF06292 �	#7FF06292�� $	��5,0q�$��2��
���   �&    Good_Teacher.Controls.FlatButton  -     Name$ Button_ExportOut��&    Good_Teacher.Controls.FlatButton   Click$ Button_ExportOut_Click��$��32��$��32��   Image_Source$= 7/Good Teacher;component/Resources/Icons/Buttons/Out.png��   
ClickBrush �	#FFF06292   Hover �	#7FF06292  -	   $ Button_EraseAll��$ Button_EraseAll_Click��$	��5,0q�$��32��$��32��$9 3/Good Teacher;component/Resources/Draw/EraseAll.png�� �	#FFF06292 �	#7FF06292