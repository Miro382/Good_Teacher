   M S B A M L   `   `   `  ���� G  CGood Teacher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulli9clr-namespace:Good_Teacher.Controls.Special.EditorSpecial+Good_Teacher.Controls.Special.EditorSpecial  +clr-namespace:Good_TeacherGood_Teacher  ;"clr-namespace:Good_Teacher.StringsGood_Teacher.Strings  =#clr-namespace:Good_Teacher.ControlsGood_Teacher.Controls      Good_Teacher.MainWindow   KEditorSpecial9clr-namespace:Good_Teacher.Controls.Special.EditorSpecial  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:Good_Teacher     6strings"clr-namespace:Good_Teacher.Strings     8controls#clr-namespace:Good_Teacher.Controls     -   %   Good_Teacher.Strings.ResStrings    AppName � Title# Z  $
��1024��$	��768��$	��940��$	��620�� � WindowStartupLocation$ CenterScreen=� � WindowState$ 	Maximized=�'  !System.Windows.Shell.WindowChrome   WindowChrome      CaptionHeight$ 0Q�   ResizeBorderThickness$ 5q�   GlassFrameThickness$ 0,0,0,1q� � 	Resources 	 �� MergedDictionaries
 �� Source�� 	 �� $
 ResourceDictionary.xaml?�%    .���� -    ��Name$ MainWindow_Border��
���   �$��1q�._�� ����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF607D8B��� $ 	#FF90A4AE�� � Offset$ 1Q�.+�� -    �Name$ 
HeaderLine�� K� ZIndex$ 10��$��40��$	��Top=� �� BitmapScalingMode$
 Fant=�����  �� ImageSource$' !Resources/Background/TitleBar.jpg�� j� TileMode$
 Tile=� j� Stretch$ UniformToFill=�.+��� -    ��Name$ 
QuitButton��   CloseImg#��[  $��Right=�$	��Top=�$��32��$��32�����	#00707070
������ $��0,3,4,0q�$��1,1,1,5q�   Close � ToolTip# Z ����  �� 
TargetType" ��.���  �� Property � �� �� Value ��  �� 
TargetType" ��.R��� #��z��._��� $��Center=�$��Center=� � Triggers :�  :� RoutedEvent$ Button.MouseEnter��.[��� .���� .���� 	 �� To$	 RedX�  �� TargetProperty$1  +(Button.Background).(SolidColorBrush.Color)�! g� FillBehavior$! HoldEnd=�" g� Duration$" 0:0:0.3D�:� $ Button.MouseLeave��.[��� .���� .���� $ TransparentX�$1  +(Button.Background).(SolidColorBrush.Color)�$! HoldEnd=�$" 0:0:0.5D��� -   $ MaximizeButton��  maximize_NormalImg#��[ $��Right=�$	��Top=�$��32��$��32�����	#00707070$��0,3,38,0q�$��1,1,1,5q����	#00000000#   RestoreDown# Z# ���� " ��.���  � �� �� " ��.R��� #��z��._��� $��Center=�$��Center=� :� $ Button.MouseEnter��.[��� .���� .���� $ CornflowerBlueX�$1  +(Button.Background).(SolidColorBrush.Color)�$! HoldEnd=�$" 0:0:0.3D�:� $ Button.MouseLeave��.[��� .���� .���� $ TransparentX�$1  +(Button.Background).(SolidColorBrush.Color)�$! HoldEnd=�$" 0:0:0.5D��� -   $ MinimizeButton��  MinimizeImg#��[ $��Right=�$	��Top=�$��32��$��32�����	#00707070$��0,3,72,0q�$��1,1,1,5q����	#00000000$   Minimize# Z$ ���� " ��.���  � �� �� " ��.R��� #��z��._��� $��Center=�$��Center=� :� $ Button.MouseEnter��.[��� .���� .���� $ 	CadetBlueX�$1  +(Button.Background).(SolidColorBrush.Color)�$! HoldEnd=�$" 0:0:0.3D�:� $ Button.MouseLeave��.[��� .���� .���� $ TransparentX�$1  +(Button.Background).(SolidColorBrush.Color)�$! HoldEnd=�$" 0:0:0.5D��� $
��Left=�$��6,4,0,4q�$��32��$��Resources/test.png��$��UniformToFill=�$��32��$	��Top=��� % �� Orientation$% 
Horizontal=�$��42,0,0,0q�.+��� #��Z  $
��Left=�
�������$��14(�$��Center=��� -   & ��Name$& 
L_FileName��$�� ��$
��Left=�
�������$��14(�$��Center=�$��Italic$�`� $ 5��$��0,40,0,0q�$��28��$	��Top=�.�c� -   ' c�Name$' Toolbar_Main��( `� IsLocked( . ) c� Band$) 1��* c� 	BandIndex$* 1��$��5,0,0,0q����	#00EEF5FD.@�o� ���	#00000000+ c� OverflowMode$+ Never=�.@�m� ,   File#��Z, .@�m� -	   - m�Name$- MenuItem_New��
.   New#��Z.  
 NewImg/ m� Icon#/ [ &    Good_Teacher.Strings.HardStrings
0   New1 m� InputGestureText#1 Z0 m� -
   $- MenuItem_Open��2   Open#��Z2   OpenImg#/ [ 3   Open#1 Z3 �� m� -   $- MenuItem_Save��4   Save#��Z4   SaveImg#/ [ 5   Save#1 Z5 m� -   $- MenuItem_SaveAs��6   SaveAs#��Z6 �� m� -   $- MenuItem_Print��7   Print#��Z7   PrintSmallImg#/ [ 8   Print#1 Z8 m� -   $- MenuItem_Export��9   Export#��Z9 / �� $��16��$��16��$��Resources/Menu/export.png��.@�m� :   Image#��Z:   
ImageSmall#/ [ .@�m� ;   CurrentPage#��Z;   OnePage#/ [ .@�m� -    	 FileImageO2#/ [	 (   "Good_Teacher.Strings.FormatStrings
<   Png#��Z< 
= � Tag$= OnePng��m� -    
 
FileImageO#/ [
 
>   Jpg#��Z> $= OneJpg��m� ?   AllPages#��Z?   AllPages#/ [ .@�m� -     
FileImage2#/ [ #��Z< $= AllPng��m� -     	FileImage#/ [ #��Z> $= AllJpg��m� -   
@   PDF#��Z@ / �� $��16��$��16��$-��'Resources/Icons/Buttons/PDFDocument.png��$
 Fant=�m� -     CopyToClipboard#/ [ A   CopyToClipboard#��ZA $= OneClipboard��m� -   $- MenuItem_Build��B   Build#��ZB / �� $��16��$��16��$��Resources/Menu/Build.png��$
 Fant=��� m� -   $- MenuItem_Info��C   FileInfo#��ZC $
 Fant=�/ �� $��16��$��16��$(��"Resources/Specific/Information.png���� m� -   $- MenuItem_Close��D   Exit#��ZD   ClosesmallImg#/ [ m� E   EditMenu#��ZE .@�m� -   $- Undo_MI��{�.  F   UndoN#��ZF G   Undo#1 ZG $
 Fant=�/ �� $!��Resources/Specific/Undo.png��$��16��$��16��$
 Fant=�m� -   $- Redo_MI��{�.  H   RedoN#��ZH I   Redo#1 ZI $
 Fant=�/ �� $!��Resources/Specific/Redo.png��$��16��$��16���� m� -   $- Cut_MI��
J   Cut#��ZJ 
K   Cut#1 ZK $
 Fant=�/ �� $ ��Resources/Specific/Cut.png��$��16��$��16��m� -   $- Copy_MI��L   Copy#��ZL M   Copy#1 ZM $
 Fant=�/ �� $!��Resources/Specific/Copy.png��$��16��$��16��m� -   $- Paste_MI��N   Paste#��ZN O   Paste#1 ZO $
 Fant=�/ �� $"��Resources/Specific/Paste.png��$��16��$��16��m� 
P   Add#��ZP .@�m� -   Q   Page#��ZQ $
= Form��/ �� $!��Resources/Icons/OnePage.png��$��16��$��16��$
 Fant=�m� -   R   InsertPageSelected#��ZR $
= Form��/ �� $#��Resources/Menu/InsertPage.png��$��16��$��16��$
 Fant=��� m� -   S   
Animations#��ZS $= 0��$
 Fant=�/ �� $#��Resources/Menu/Animations.png��$��16��$��16��m� #��ZQ .@�m� -    	T   Up#��ZT $= 0��/ �� $ ��Resources/Icons/moveup.png��$��16��$��16��m� -!   U   Down#��ZU $= 1��/ �� $"��Resources/Icons/movedown.png��$��16��$��16���� m� -"   V   DuplicatePage#��ZV $= 3��/ �� $#��Resources/Icons/duplicate.png��$��16��$��16���� m� -#   $- MenuItem_MovePage��W   MovePage#��ZW $= 5��/ �� $��16��$��16��$(��"Resources/Icons/Buttons/MoveTo.png��$
 Fant=�m� -$   $- MenuItem_Lock��X   
LockCanvas#��ZX $= 6��/ �� $��16��$��16��$9��3/Good Teacher;component/Resources/Specific/Lock.png��m� -%   $- MenuItem_Hide��Y   HidePage#��ZY $= 7��/ �� $��16��$��16��$@��:/Good Teacher;component/Resources/Icons/Buttons/Hidden.png��$
 Fant=��� m� -&   $- MenuItem_Settings��Z   	Customize#��ZZ $= 4��/ �� $��16��$��16��$?��9/Good Teacher;component/Resources/Icons/SettingsBlack.png��$
 Fant=��� m� -'   [   Delete#��Z[ $= 2��/ �� $ ��Resources/Icons/remove.png��$��16��$��16���� m� -(   \   Clear#��Z\ / �� $��Resources/Menu/Clear.png��$��16��$��16��m� 
]   Run#��Z] .@�m� -)   #��Z] $= 0��
^   Run#1 Z^ / �� $
 Fant=�$��Resources/Menu/Play.png��$��16��$��16��m� -*   _   
RunCurrent#��Z_ $= 0��`   
RunCurrent#1 Z` / �� $
 Fant=�$$��Resources/Menu/PlayCurrent.png��$��16��$��16��m� a   Tools#��Za .@�m� -+   b   ListControls#��Zb $= 0��/ �� $��Resources/Menu/list.png��$��16��$��16��m� -,   c   Archive#��Zc $= 0��/ �� $ ��Resources/Menu/Archive.png��$��16��$��16��m� --   d   	Repeating#��Zd $= 0��/ �� $*��$Resources/Menu/RepeatingControls.png��$��16��$��16��m� -.   e   RealView#��Ze f   RealView#1 Zf $= 0��/ �� $*��$Resources/Icons/Buttons/RealView.png��$��16��$��16��m� -/   g   FontPackage#��Zg $= 0��/ �� $$��Resources/Menu/FontPackage.png��$��16��$��16��m� -0   h   Transitions#��Zh $= 0��/ �� $,��&Resources/Icons/Buttons/Transition.png��$��16��$��16��m� -1   i   Timers#��Zi $= 0��/ �� $'��!Resources/Icons/Buttons/Timer.png��$��16��$��16��m� -2   $
 Fant=�j   Script#��Zj $= 0��/ �� $��Resources/Menu/Script.png��$��16��$��16��m� k   Application#��Zk .@�m� -3   l   AppSettings#��Zl $= 0��/ �� $$��Resources/Menu/AppSettings.png��$��16��$��16���� m� -4   $- MenuItem_About��m   About#��Zm m� -5   n   CheckForUpdates#��Zn $
 Fant=�$= 0��/ �� $$��Resources/Menu/CheckUpdate.png��$��16��$��16��$��Uniform=��� m� -6   o   GoodTeacherRepairo#��Zo $
 Fant=�$= 0��/ �� $(��"Resources/Menu/testRepairoGray.png��$��16��$��16��m� p   Help#��Zp .@�m� -7   q   Manual#��Zq $= 0��r   Help#1 Zr / �� $
 Fant=�$!��Resources/Manual/Manual.png��$��16��$��16��� $ 2��$
��Left=�$��	0,131,0,0q�$	��180�����#bdc3c7.+��� -8   s ��Name$s ScrollViewer_TestList��$��Visible=�.��$� -9   t $�Name$t Stack_TestList��u %� VirtualizationMode$u 	Recycling=����	#FFBDC3C7v � ContextMenuv �� .@�m� -:   w   AddPage#��Zw / �� $
 Fant=�$��16��$��16��$ ��Resources/Menu/AddPage.png���� m� -;   #��ZR / �� $
 Fant=�$��16��$��16��$#��Resources/Menu/InsertPage.png�� -   'Good_Teacher.Controls.TestType_ControlN��%3   '        �� "  .���  � ��$ 0,10,0,6q��� $ 4��
���   �$��1q�$��63��$��
-1,68,-1,0q�$	��Top=����	#FFECF0F1x J� Effectx 3  -System.Windows.Media.Effects.DropShadowEffect  y   Opacity$	y 0.2Q�._�� z � ColumnDefinitionsz �� �� $
��Auto �.+��� -<   { ��Name${ SP_MenuPanelControls��$y�	Collapsed=�$
 Fant=�$% Vertical=�$��1��$��36��$��Center=�.+�=   7Good_Teacher.Controls.Special.EditorSpecial.ImageButton  -=   |  Name$| ImgB_NextActions��=   7Good_Teacher.Controls.Special.EditorSpecial.ImageButton}   	MouseDown$&}  ImageButtonNextActions_MouseDown��$��22��$��22��$��5,0,0,0q�$
��Left=�~   NextC# Z~    DefaultBrush$( "Resources/Menu/Panel/NextPanel.png���   Hover$-� 'Resources/Menu/Panel/NextPanelHover.png���   
ClickBrush$-� 'Resources/Menu/Panel/NextPanelClick.png��  ->   $| ImgB_PreviousActions��$*} $ImageButtonPreviousActions_MouseDown��$��22��$��22��$��5,5,0,0q�$
��Left=��   	PreviousC# Z� $, &Resources/Menu/Panel/PreviousPanel.png��$1� +Resources/Menu/Panel/PreviousPanelHover.png��$1� +Resources/Menu/Panel/PreviousPanelClick.png���� -?   $s SCRV_MenuOptions��$��Disabled=�$��Hidden=�.���� -@   ${ SP_ActionControlPanel��$% 
Horizontal=�$��0�� ��%/   '��       �� " ��.���  � ��$
 10,4q�.+�o� ���	#00F0F0F0.@�m� -A   $��10,0,0,0q����� .+��� $��32��$��32��$ ��Resources/Menu/AddPage.png���� #��Zw m� -B   $- Item_Export��$��10,0,0,0q����� .+��� $��32��$��32��$��Resources/Menu/export.png���� #��Z9 $��Center=��� $��10��$��10��$��Resources/Down.png��.@�m� #��Z: / �� $��16��$��16��$'��!Resources/EditValues/AddImage.png��.@�m� #��Z; / �� $��16��$��16��$!��Resources/Icons/OnePage.png��.@�m� -C   #��Z< $= OnePng��/ �� $��16��$��16��$$��Resources/Icons/FileImage2.png��m� -D   #��Z> $= OneJpg��/ �� $��16��$��16��$#��Resources/Icons/FileImage.png��m� #��Z? / �� $��16��$��16��$��Resources/Icons/Pages.png��.@�m� -E   #��Z< $= AllPng��/ �� $��16��$��16��$$��Resources/Icons/FileImage2.png��m� -F   #��Z> $= AllJpg��/ �� $��16��$��16��$#��Resources/Icons/FileImage.png��m� -G   #��Z@ / �� $��16��$��16��$-��'Resources/Icons/Buttons/PDFDocument.png��$
 Fant=�m� -H   #��ZA $= OneClipboard��/ �� $��16��$��16��$%��Resources/Icons/ToClipboard.png��m� -I   $- 
Item_Build��$��10,0,0,0q����� .+��� $��32��$��32��$��Resources/Menu/build.png���� #��ZB $��Center=�m� -J   $��10,0,0,0q����� .+��� $��32��$��32��$��Resources/Menu/Print.png��$
 Fant=��� #��Z7 $��Center=�m� -K   $- Item_Run���   RunProgramTooltip# Z� $��10,0,0,0q����� .+��� $��32��$��32��$��Resources/Menu/Play.png���� #��Z] $��Center=�m� -L   $- Item_ControlList��$��10,0,0,0q����� .+��� $��32��$��32��$��Resources/Menu/list.png���� #��Zb $��Center=�m� -M   $- Item_ClearList��$��10,0,0,0q����� .+��� $��32��$��32��$��Resources/Menu/Clear.png���� #��Z\ $��Center=�m� -N   $- Item_Settings��$��10,0,0,0q����� .+��� $��32��$��32��$!��Resources/Menu/Settings.png���� �   Settings#��Z� $��Center=�m� -O   $- Item_Archive��$��10,0,0,0q����� .+��� $��32��$��32��$ ��Resources/Menu/Archive.png���� #��Zc $��Center=�m� -P   $- Item_Animations��$��10,0,0,0q����� .+��� $��32��$��32��$#��Resources/Menu/Animations.png��$
 Fant=��� #��ZS $��Center=�m� -Q   $- Item_RepeatingControls��$��10,0,0,0q����� .+��� $��32��$��32��$*��$Resources/Menu/RepeatingControls.png��$
 Fant=��� #��Zd $��Center=�m� -R   $- Item_FontPackage��$��10,0,0,0q����� .+��� $��32��$��32��$$��Resources/Menu/FontPackage.png��$
 Fant=��� #��Zg $��Center=�m� -S   $- Item_Transitions��$��10,0,0,0q����� .+��� $��32��$��32��$,��&Resources/Icons/Buttons/Transition.png��$
 Fant=��� #��Zh $��Center=�m� -T   $- 
Item_Timer��$��10,0,0,0q����� .+��� $��32��$��32��$'��!Resources/Icons/Buttons/Timer.png��$
 Fant=��� #��Zi $��Center=�� $ 3��$��85��$��180,131,0,0q�$	��Top=����	#FFECEFF1x   $	y 0.4Q�.+� � -U   �  �Name$� FrameEditor��$�� ���  � NavigationUIVisibility$� Hidden=��� $ 2��
���   �$��1q�$��0,216,-1,-1q�$��Right=�$	��150��._�� ���	#FFECEFF1.+��� $��Visible=�.�� � -V   $� ValueEditor��$� Hidden=��� -W   $s ScrollViewerZoom��$��184,228,150,32q�$��Disabled=�$��Disabled=�.��(� -X   � (�Name$� 	viewboxDC��.���� -Y   $ BorderWindowSize��$��0,0,5,5q�
���   �$��1q�
�������$
��1282��$	��722��._�� .+��� $ -1��$��	5,5,-4,-4q����	#7F000000�� $ 0��
��������� -Z   � ��Name$� DesignCanvas��$ 1��
��������� ���	#7F000000$��1q�$
��Left=�$��28��$��	180,0,0,0q�$��Bottom=�$	��315��� �� CornerRadius$� 0,15,0,0�����	#4CFFFFFF._�� $	��5,0q�$
��Left=�$��Center=�.+�(	   "Good_Teacher.Controls.SelectButton	  -[   � 	 Name$� 
LockButton��(
   "Good_Teacher.Controls.SelectButton� 
  OnCheckChanged$� LockButton_OnCheckChanged��$��24��$��24��$��Bottom=�$
��Left=�� 	  Image_Source$!� Resources/Specific/Lock.png��� 	  Hover� �	#7FF1C40F� 	  	OnChecked� �	#FFF1C40F# ZX 	  -\   $� HiddenPageButton��$%� SecretPageButton_OnCheckChanged��$	��2,0q�$��24��$��24��$��Bottom=�$
��Left=�$(� "Resources/Icons/Buttons/Hidden.png��� �	#7FF1C40F� �	#FFF1C40F# ZY �� -]   � ��Name$� 
SliderZoom��$	��3,0q�$	��120��$��24��$��Bottom=�$
��Left=�� � Maximum$
� 1000Q�� � Minimum$� 10Q�� � SmallChange$� 1Q�� �� TickFrequency$	� 100Q�� �� TickPlacement$� TopLeft=�� �� AutoToolTipPlacement$� TopLeft=�� � Value$	� 100Q��� -^   � ��Name$� TextBox_Zoom��$��Bottom=�$��20��� �� 	MaxLength$� 3��� �� TextWrapping$� NoWrap=�$	��100��$��40��$
��Left=�$��Center=�$��Center=����	#7FFFFFFF&    Good_Teacher.Controls.FlatButton  &    Good_Teacher.Controls.FlatButton�   Click$ � FlatButtonZoomCancel_Click��$��22��$��3,0,0,0q�$��Bottom=�$��22��$
��Left=��   Image_Source$$� Resources/Icons/ZoomCancel.png���   Hover� �	#7FFFFFFF�   
ClickBrush
� ������   DefaultZoom# Z� 	  -_   $� CanvasOnlyButton��$%� CanvasOnlyButton_OnCheckChanged��$��24��$��24��$��1,0,0,0q�$��Bottom=�$
��Left=�$(� "Resources/Icons/CanvasDrawOver.png��� �	#7FFFFFFF� �#ED4C67�   
OnlyCanvas# Z� 	  -`   $� 
MoveOnGrid��$� MoveOnGrid_OnCheckChanged��$��24��$��24��$��1,0,0,0q�$��Bottom=�$
��Left=�$� Resources/Icons/Grid.png��� �	#7FFFFFFF� �#ED4C67�   AlignmentToGrid# Z� �� -a   $ Notification��$y�	Collapsed=�$��1q�$� 	10,10,0,0��$��Right=�$��35��$��	0,0,155,0q�$��Bottom=����	#FFE91E63
���   �x   $	y 0.6Q�._��� -b   $& NotificationLabel��$��5,0,5,0q�$��Center=�$�� ��$��18(�$��Center=�
��������� -c   $ B_DefaultCanvasPosition��$y�	Collapsed=�
���   �$��1q�$
��Left=�$��28��$��	500,0,0,0q�$��Bottom=�$��33��$� 5,5,0,0�����	#4CFFFFFF._�  $+� %FlatButtonDefaultCanvasPosition_Click��$��2q�$'� !Resources/Specific/CancelMove.png��� �	#7FFFFFFF
� ������   DefaultCanvasPosition# Z� 