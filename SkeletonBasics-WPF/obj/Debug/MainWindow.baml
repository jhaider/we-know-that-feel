   M S B A M L   `   `   `  ����  SkeletonBasics-WPF8    2Microsoft.Samples.Kinect.SkeletonBasics.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Skeleton Basics��5   	   $	��735��6!   $	��770��6.    � 	Resources 5         MediumGreyBrush �� Color  KinectPurpleBrush  KinectBlueBrush �� 
TargetType �� Property �� Value  SquareCheckBox �� 
TargetType �� Orientation �Name	 ��Name
 ��Name �� TextAlignment �� Triggers P� Property P� Value �� 
TargetName��%1  &	        &	 #     &	 F     '�� i     &	 �     7  7  �� 5   
   $ 	#ff6e6e6e��62   �� 5   
   $ 	#ff52318f��64   �� 5	   
   $ 	#ff00BCF2��62   �� 5
   
   " ��6   .�5      ��  �@J�SnapsToDevicePixels . 64   5      �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� $ 
Horizontal=�6)   
������ .+�5   "   � $ SquareCheckBoxChecked��6'   .+�5   &   �� $	 CheckedNormal��6,   $��Images\CheckedNormal.png��6C   $
��None=�6e   $��Center=�6t   �� 5   &   $	 CheckedHover��6,   $��Images\CheckedHover.png��6B   $
��None=�6c   $��Center=�6r   $y�	Collapsed=�6�   5   #   � 5   "   $ SquareCheckBoxUnchecked��6'   $y�	Collapsed=�6H   .+�5   &   �� $	 UncheckedNormal��6,   $ ��Images\UncheckedNormal.png��6E   $
��None=�6i   $��Center=�6x   �� 5   &   $	 UncheckedHover��6,   $��Images\UncheckedHover.png��6D   $
��None=�6g   $��Center=�6v   $y�	Collapsed=�6�   5   #   �� 5   "   $
 SquareCheckBoxText��6,   #��z��6H   $
 Left=�6i   $��Center=�6   8��  $��15(�6�   $��9,0,0,0q�6�   5      5       5      P� 5       �@d�	IsChecked$ falseY�6;   . �5    "   �� $ SquareCheckBoxChecked��6Q    � y�$ 	Collapsed=�6?   6Q   �� 5!   "   $ SquareCheckBoxUnchecked��6O    � y�$ Visible=�6?   6O   5"      P� 5#       �@J�IsMouseOver . . �5$   "   �� $ CheckedNormal��6Q    � y�$ 	Collapsed=�6?   6Q   �� 5%   "   $ UncheckedNormal��6Q    � y�$ 	Collapsed=�6?   6Q   �� 5&   "   $ CheckedHover��6O    � y�$ Visible=�6?   6O   �� 5'   "   $ UncheckedHover��6O    � y�$ Visible=�6?   6O   �� 5(   "   8  $ SquareCheckBoxText��6h    � ��5)      5+      5-      5.      5/      .��51      � -   5        $��
layoutGrid��51      $��	10 0 10 0q�6    � RowDefinitions 52   
   �� 53      $
��Auto �6   �� 54      $��* �6   �� 55      $
��Auto �6   �� 56      $
��Auto �6   .+�58   
   ]� $��0��6   $��0,0,0,20q�6!   .+�5:      �� $��0 0 -1 0q�6   $	��Top=�6+   #��[  6C   $��Segoe UI)�6q   $��18(�6�   �� 6�   $�Regular Expressions��6�   �� 6�   5<      �� 6    65   5C      �� 5D   
   $
��Wrap=�6   $��Gesture Detected:��6(   $	��142��6A   $��16(�6M   $��368,2,232,588q�6[   $��2��6r   �� 5E   
   -   5        $
 DetectedGestureBlock��5E      $
��Wrap=�62    J� RenderTransformOrigin$ 0.5,0.5&�6F   $	��232��6f   $��Right=�6r   $��14(�6�   $��0,4,0,6q�6�   ���	#FF57A42Bz�5F      V� 5G      .~�5H      �� �� 5I      T� 5J      5K      5M      (� 5N   
   $��1��6    (� Stretch$ Uniform=�6   $��Center=�61   .��5O      �� -   5        $��Image��5O      $	��640��6!   $	��480��6-   5P      �� 5R   
   -   5        $��checkBoxSeatedMode��5R   �   $��2��6   #��[ 6    $��Seated Mode��6H   $
��Auto��6^   $
��Left=�6l   $��Center=�6�   $��
0 10 10 10q�6�   6�   �� 5S   
   -   5        $��	statusBar��5S   ?   $��3��6   $��Stretch=�6!   $��Bottom=�6P   
�������#��[  6~   .@�5T      �� $��0 0 0 10q�6   .��5U      �� -   5        $��statusBarText��5U      $��-1 0 0 0q�61   20Click 'Seated' to change skeletal pipeline type!6u   5V      5W      �� 5X   
   -   5         ��Name$ RecordButton��5X      $��Record��6'   $
��Left=�68   $��
240,10,0,0q�6S   $��2��6g   $	��Top=�6t   $��75��6�   $��24��6�   ���	#FFEE5C63$��0q�6�   �� 5Y   
   -	   5        $ DetectButton��5Y      $��Detect��6'   $
��Left=�68   $��
541,10,0,0q�6S   $��2��6g   $	��Top=�6t   $��75��6�   $��24��6�   ���	#FFB284E0$��0q�6�   �� 5Z   
   -
   5        $
 RecordingBlock��5Z      $
��Left=�6,   $��
334,14,0,0q�6G   $��2��6[   $
��Wrap=�6h   $��Recording...��6|   $	��Top=�6�   $��20��6�   $��80��6�   �� 5[   
   -   5        $
 DetectingBlock��5[      $
��Left=�6,   $��
631,14,0,0q�6G   $��2��6[   $
��Wrap=�6h   $��	Detecting��6|   $	��Top=�6�   $��19��6�   $��68��6�   5\      5]      