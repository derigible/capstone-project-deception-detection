   M S B A M L   `   `   `  ����G  CKinect_D2_v1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Kinect_D2_v1Kinect_D2_v1  5   	   | xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting 5   	   S OMicrosoft.Kinect.Toolkit, Version=1.6.0.0, Culture=neutral, PublicKeyToken=nulleHclr-namespace:Microsoft.Kinect.Toolkit;assembly=Microsoft.Kinect.ToolkitMicrosoft.Kinect.Toolkit 5   	   ^ ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers 5   	       Kinect_D2_v1.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ,localclr-namespace:Kinect_D2_v1     5   	   �chartingToolkit{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5   	   \toolkitHclr-namespace:Microsoft.Kinect.Toolkit;assembly=Microsoft.Kinect.Toolkit     5   	   mkt^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     5   	   -   5          � Title$#  Deception Detection - Capture��5   	   $	��572��6/   $	��997��6<    � WindowStartupLocation$ Manual=�6H    � Left$ 0��5	   	   
 � Top$ 0��6    � Icon$ Images/video.png��6H    � 	Resources 5
       �� MergedDictionaries �� Source   MediumGreyBrush �� Color  KinectPurpleBrush  KinectBlueBrush	 �� 
TargetType
 �� Property �� Value  SquareCheckBox �� 
TargetType �� Orientation �Name ��Name ��Name �� TextAlignment �� Triggers P� Property P� Value �� 
TargetName�� 5   
    5      �� 5      $f `pack://application:,,,/Microsoft.Samples.Kinect.WpfViewers;component/KinectControlResources.xaml?�6%   %1  &	        &	 #     &	 F     '�� i     &	 �     7  7  �� 5      $ 	#ff6e6e6e��66   �� 5      $ 	#ff52318f��68   �� 5      $ 	#ff00BCF2��66   �� 5      "	 ��6   .�5      �� 
 �@J�SnapsToDevicePixels . 68   5      �� 5      "	 ��6   .�5      �� 
 � �� 5      �� 5      " ��6*   .R�5      � .+�5   "   �� $ 
Horizontal=�6-   
������ .+�5   &   � $ SquareCheckBoxChecked��6+   .+�5   *   �� $ CheckedNormal��60   $��Images\CheckedNormal.png��6G   $
��None=�6i   $��Center=�6x   �� 5   *   $ CheckedHover��60   $��Images\CheckedHover.png��6F   $
��None=�6g   $��Center=�6v   $y�	Collapsed=�6�   5   '   � 5   &   $ SquareCheckBoxUnchecked��6+   $y�	Collapsed=�6L   .+�5    *   �� $ UncheckedNormal��60   $ ��Images\UncheckedNormal.png��6I   $
��None=�6m   $��Center=�6|   �� 5!   *   $ UncheckedHover��60   $��Images\UncheckedHover.png��6H   $
��None=�6k   $��Center=�6z   $y�	Collapsed=�6�   5"   '   �� 5#   &   $ SquareCheckBoxText��60   #��z��6L   $
 Left=�6m   $��Center=�6�   8��  $��15(�6�   $��9,0,0,0q�6�   5$   #   5%       5&      P� 5'   "    �@d�	IsChecked$ falseY�6?   . �5(   &   �� $ SquareCheckBoxChecked��6U   
 � y�$ 	Collapsed=�6C   6U   �� 5)   &   $ SquareCheckBoxUnchecked��6S   
 � y�$ Visible=�6C   6S   5*   #   P� 5+   "    �@J�IsMouseOver . . �5,   &   �� $ CheckedNormal��6U   
 � y�$ 	Collapsed=�6C   6U   �� 5-   &   $ UncheckedNormal��6U   
 � y�$ 	Collapsed=�6C   6U   �� 5.   &   $ CheckedHover��6S   
 � y�$ Visible=�6C   6S   �� 5/   &   $ UncheckedHover��6S   
 � y�$ Visible=�6C   6S   �� 50   &   8  $ SquareCheckBoxText��6l   
 � ��51   #   53      55      56      57      .��59      � -   5        $ 
layoutGrid��59      $��0,0,2,0q�6'   $	��800��68   $	��Top=�6E    � RowDefinitions 5:      �� 5<      $
��Auto �6$   $��47��62   �� 5>      $
��268* �6$   �� 5@      $
��485* �6$    � ColumnDefinitions 5C      �� 5E      $
��605* �6'   $	��450��64   �� 5F      $
��479* �6'   .+�5H      4  .Microsoft.Kinect.Toolkit.KinectSensorChooserUI  -   5          Name$ SensorChooserUI��5H   0      IsListening . $��Center=�6]   $	��Top=�6z   $��40��6�   $��253,0,258,0q�6�   $��40��6�   ]� 5I      $��0��6   $��2��6)   $��	10,0,11,0q�6=   $��47��6P   $	��Top=�6\   .+�5J      �� -   5         ��Name$ btnBack��5J      $
��Left=�6&   $��4,14,0,0q�6A   $	��Top=�6S   $��20��6k   $��0q�6�    J� Opacity$ 0.995Q�6�   $��20��6�   ��6�   Z� ��6�   Z� ��6�   Z� .��5K      �� $��Images\arrow-left.png��6   5L      � 5M      $��60��6   $	��Top=�6   $��	10,7,10,0q�67   $	��Top=�6J   .+�5N      �� $��37��6   $��
10,0,837,0q�6$   $	��Top=�68   $��Images/Logo.png��6P   �� 5O      -   5         ��Name$ 	statusBar��5O      $
��Left=�6/   $	��374��6J   $��
562,0,0,23q�6V   .@�5P      �� $��23��6$   $	��Top=�60   $��0,10,0,0q�6H   $	��908��6Z   .��5Q      �� -   5        $ statusBarText��5Q   $   $
��Wrap=�6;   $�� ��6O   $	��622��6W   $��17��6c   5R      5S      5T      5U      � 5W      $��0��6   $��1��6'   $��
10,10,0,10q�64   $
��Left=�6H   $	��509��6c   .+�5X      � -   5        $��MainViewerHost��5X      .+�5Y      � -   5        $��ColorVis��5Y      #��� ��6/   .+�5Z      (� $��Center=�6&   $��Center=�6C    (� Stretch$ Uniform=�6^   .��5\   "   � .+�5]   &   ;  5Microsoft.Samples.Kinect.WpfViewers.KinectColorViewer  -	   5          Name$ ColorViewer��5]   ;   6  0Microsoft.Samples.Kinect.WpfViewers.KinectViewer   CollectFrameRate .      RetainImageOnSensorChange  . 7  1Microsoft.Samples.Kinect.WpfViewers.KinectControl!   KinectSensorManager! 6P   �� *KinectSensorManager+6�   �� 5^   &   .+�5_   *   >  8Microsoft.Samples.Kinect.WpfViewers.KinectSkeletonViewer  "   	ImageType$" Color=�5d   %   ! 5`   %   �� *KinectSensorManager+y�5a   %   �� *(&KinectSensorManager.ColorStreamEnabled+  BoolToVisibilityConverter# �� 	Converter## [ ��5b   %   �� $ �� ElementName$$ ColorViewer��% �� Path$% ActualWidth���5c   %   �� $$ ColorViewer��$% ActualHeight�5d   %   5e   '   5f   #   5g      �� 5h        LabelForegroundBrush#��[ 5i      $��Right=�5j      $	��Top=�65   #��� ��5k      $��50��5l      $��50��6$   ._�5m   "   �� $ Vertical=�6-   .+�5n   &   ��   HeaderFontSize#��[ 60   $��Center=�6�   $��-2q�6�   ��6[   �� $$ ColorViewer��$% 	FrameRate�6�   �� 5o   &     FPSFontSize#��[ 60   $��Center=�6X   $��-2q�6u   FPS6�   5p   #   5q      �� 5r      ���#7777& J� SnapsToDevicePixels& . y�65   �� *(&KinectSensorManager.ColorStreamEnabled+## [ ' �� ConverterParameter$
' True��6�   5s      5t      � 5u      -
   5        ( �Name$( 
waitScreen��5u   (   $��
10,10,0,13q�6   $
��Left=�6<   $	��Top=�6W   $	��194��6o   $	��200��6{   ��.  ) J� OpacityMask) �	#FFF9F7F7.��5v      �� -   5        * ��Name$* popuptxt��5v      $��Ho there��6*   $��24,-223,9,389q�6=   + J� RenderTransformOrigin$+ 
0.86,1.288&�6T   5w      5x      � 5~      $��2��6   $��0��6$   #��� ��64   $��	10,29,0,0q�5      $	��Top=�5�      $	��151��6'   $
��Left=�64   $	��509��6O   .+�5�      �� -   5        $ 	startStop��5�      $��
0,30,30,54q�6(   $��Right=�6V   $��80��6r   .��5�      �� -   5        $ imgStartStop��5�      $��/Images/record.png��6.   $��40��6J   $��44��6V   5�      �� 5�      -   5        , ��Name$, lstTags��5�      $��
24,30,0,10q�6'   $
��Left=�6\   $	��119��6w   �� 5�      -   5        $, 	lstTagLog��5�      $	��111��6)   $��
0,30,146,0q�66   $	��Top=�6J   $��Right=�6b   $	��199��6~   �� 5�      -   5        $ btnTag��5�      $��	Tag Again��6%   $
��Left=�69   $��24,3,0,0q�6T   $	��Top=�6f   $��77��6~   6�   �� 5�      -   5        $ btnRemoveTag��5�      $��Remove��6+   $��Right=�6<   $��	0,3,270,0q�6X   $	��Top=�6k   $��75��6�   6�   �� 5�      -   5        - ��Name$- recordVideoBool��5�      $��Record Video��60   $
��Left=�6G   $��377,102,0,0q�6b   $	��Top=�6w   $��19��6�   $	��111��6�   6�   �� 5�      -   5        $- checkBoxSeatedMode��5�      $��Seated Mode��63   $
��Left=�6I   $��377,123,0,10q�6d   $	��115��6z   6�   5�      � 5�   
   $��1��6   $��2��6   $��1��6-   .+�5�      8  2Microsoft.Samples.Kinect.WpfViewers.KinectSettings  $��0 20 0 0q�6�   & . ! 6(   �� *KinectSensorManager+.   DepthTreatment. 6\   �� *DepthTreatment+/ �� Mode$/ TwoWay=�6�   5�      5�      5�      