   M S B A M L   `   `   `  ���� E  AHealthyTwo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:HealthyTwo
HealthyTwo  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem > :Lib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null&clr-namespace:Lib;assembly=LibLib     HealthyTwo.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:HealthyTwo     9System&clr-namespace:System;assembly=mscorlib     .srcclr-namespace:Lib;assembly=Lib     -     �Name$  winMain�� � Title$ Healthy Two™��$	��466��$	��800�� � 
ResizeMode$ CanMinimize=�$y�Visible=� � AllowsTransparency .  � WindowStyle$
 None=� � DataContext ��  �� RelativeSource �� *Self+ � 	Resources    
LabelHover �� Triggers	 :� RoutedEvent
 �� TargetProperty	 �� To g� Duration  
ImgFadeOut W� From	 W� To  PageFadeOut g� 	BeginTime R� KeyTime R� Value  System.Windows.Visibility��%�  &	        &	     &	 �    ��  :� $	 Mouse.MouseEnter��[��� .���� $*
 $(Foreground).(SolidColorBrush.Color)�.���� $ 	#fff17250X�$ 
0:0:0:0.14D�:� $	 Mouse.MouseLeave��[��� .���� $*
 $(Foreground).(SolidColorBrush.Color)�.���� $ WhiteX�$ 
0:0:0:0.14D���  :� $	 Mouse.MouseEnter��[��� .���� $
 Opacity�.��W� $	 1.0X�$	 0.0X�$ 
0:0:0:0.14D�:� $	 Mouse.MouseLeave��[��� .���� $
 Opacity�.��W� $	 0.0X�$	 1.0X�$ 
0:0:0:0.14D��� .��T� $ 0:0:0X�$
 
Visibility�..�i� $ 0��  Visible=�W� $ 0:0:0.0X�$
 Opacity�$ 1X�$ 0X�$ 0:0:0.2D�����  �� ImageSource$ 
img/bg.png��.��� -    �Name$ grdMain��.+��� -    ��Name$ imgHomeHover��$��img/home-hover.png��$��16��$��16��$��16,28,768,422q��� -   $ imgHome��$��img/home.png��#��[ $��16��$��16��$��16,28,768,422q��� -   $ imgMessageHover��$��img/message-hover.png��$��16��$��16��$��40,28,744,422q��� -   $ 
imgMessage��$��img/message.png��#��[ $��16��$��16��$��40,28,744,422q��� -   $ imgEmailHover��$��img/email-hover.png��$��16��$��16��$��64,28,720,422q��� -   $ imgEmail��$��img/email.png��#��[ $��16��$��16��$��64,28,720,422q��� -	   $ imgInstagramHover��$��img/instagram-hover.png��$��16��$��16��$��32,432,752,18q��� -
   $ imgInstagram��$��img/instagram.png��#��[ $��16��$��16��$��32,432,752,18q��� -   $ imgFacebookHover��$��img/facebook-hover.png��$��16��$��16��$��56,432,728,18q��� -   $ imgFacebook��$��img/facebook.png��#��[ $��16��$��16��$��56,432,728,18q��� -   $ imgUserHover��$��img/user-hover.png��$��16��$��16��$��748,36,36,414q��� -   $ imgUser��$��img/user.png��#��[ $��16��$��16��$��748,36,36,414q��� -    ��Name$ lblAct��$��
ACTIVITIES��#��[  $
��Left=�$��	35,49,0,0q�$	��Top=�
�������$��Aller Display)�$��36(��� -   $ lblDev��$��DEVICES��#��[  $
��Left=�$��
33,113,0,0q�$	��Top=�
�������$��Aller Display)�$��36(��� -   $ lblPro��$��PROFILE��#��[  $
��Left=�$��
33,177,0,0q�$	��Top=�
�������$��Aller Display)�$��36(��� -   $ lblSum��$��	SUMMARIES��#��[  $
��Left=�$��
34,241,0,0q�$	��Top=�
�������$��Aller Display)�$��36(��� $��Hello,��$
��Left=�$��
667,35,0,0q�$	��Top=�
�������$��Nobile)�$��10(��� -   $ lblUsername��$��Loukas��$
��Left=�$��
696,35,0,0q�$	��Top=����	#FF00A6FF$��Nobile)�$��10(��� $��!��$
��Left=�$��
731,35,0,0q�$	��Top=�
�������$��Nobile)�$��10(�� -   $ grdHome��.+��� $��
WELCOME TO��$
��Left=�$��
258,73,0,0q�$	��Top=����	#FFFF192B$��Aller Display)�$��11(��� $��HEALTHY TWO™��$
��Left=�$��
326,73,0,0q�$	��Top=�
�������$��Aller Display)�$��11(��� $��TRACK YOUR ACTIVITIES��$
��Left=�$��275,111,0,0q�$	��Top=�
�������$��Aller Display)�$��24(��� $@��:Your activities are divided into easy to review categories��$
��Left=�$��275,143,0,0q�$	��Top=����	#FF00A6FF$��Nobile)�$��14(��� $��ORGANIZE YOUR DEVICES��$
��Left=�$��274,175,0,0q�$	��Top=�
�������$��Aller Display)�$��24(��� $8��2Use the full capabilities of your favorite devices��$
��Left=�$��275,207,0,0q�$	��Top=����	#FF00A6FF$��Nobile)�$��14(��� $��SHARE YOUR PROFILE��$
��Left=�$��273,239,0,0q�$	��Top=�
�������$��Aller Display)�$��24(��� $��REVIEW YOUR SUMMARIES��$
��Left=�$��274,279,0,0q�$	��Top=�
�������$��Aller Display)�$��24(��� $��img/home-clock.png��$��40��$��40��$��280,360,480,66q��� $��img/home-calendar.png��$��40��$��40��$��352,360,408,66q��� $��img/home-tag.png��$��40��$��40��$��424,360,336,66q��� $��img/home-envelope.png��$��40��$��40��$��496,360,264,66q��� $��img/bottom-corner.png��$��8��$��8��$��761,417,31,41q�� -   $ grdActivities��$y�Hidden=� J� Opacity$ 0Q�.+��� -   $ imgCloseAct��$��img/close-red.png��$��16��$��16��$��220,59,564,391q�� -   $ 
grdDevices��$y�Hidden=�$ 0Q�.+��� -   $ imgCloseDev��$��img/close-red.png��$��16��$��16��$��178,123,606,327q��� -   $ imgAddDevHover��$��img/add-hover.png��$��16��$��16��$��752,432,32,18q��� -   $ 	imgAddDev��$��img/add.png��#��[ $��16��$��16��$��752,432,32,18q�� -   $ 
grdProfile��$y�Hidden=�$ 0Q�.+��� $��PROFILE��$
��Left=�$��
283,97,0,0q�$	��Top=����	#FFFF192B$��Aller Display)�$��11(��� $��
First Name��$
��Left=�$��283,123,0,0q�$	��Top=�
�������$��Nobile)�$��10(��� $��	Last Name��$
��Left=�$��283,155,0,0q�$	��Top=�
�������$��Nobile)�$��10(��� $��	Birthdate��$
��Left=�$��283,187,0,0q�$	��Top=�
�������$��Nobile)�$��10(��� $��Postal Code��$
��Left=�$��283,219,0,0q�$	��Top=�
�������$��Nobile)�$��10(��� $��Gender��$
��Left=�$��283,251,0,0q�$	��Top=�
�������$��Nobile)�$��10(��� $��Height��$
��Left=�$��283,283,0,0q�$	��Top=�
�������$��Nobile)�$��10(��� $��Weight��$
��Left=�$��283,315,0,0q�$	��Top=�
�������$��Nobile)�$��10(��� $��Locale��$
��Left=�$��283,347,0,0q�$	��Top=�
�������$��Nobile)�$��10(��� $��Last Updated��$
��Left=�$��283,379,0,0q�$	��Top=�
�������$��Nobile)�$��10(��� $��img/bottom-corner.png��$��8��$��8��$��761,417,31,41q��� $��img/top-corner.png��$��8��$��8��$��264,80,528,378q��� -   $ imgCloseProf��$��img/close-red.png��$��16��$��16��$��174,187,610,263q��� $��img/users-dark.png��$��8��$��8��$��520,128,272,330q��� $��img/users-dark.png��$��8��$��8��$��520,160,272,298q��� $��img/calendar-dark.png��$��8��$��8��$��520,192,272,266q��� $��img/envelope-dark.png��$��8��$��8��$��520,224,272,234q��� $��img/feather-dark.png��$��8��$��8��$��520,256,272,202q��� $��img/forkandknife-dark.png��$��8��$��8��$��520,288,272,170q��� $��img/forkandspoon-dark.png��$��8��$��8��$��520,320,272,138q��� $��img/globe-dark.png��$��8��$��8��$��520,352,272,106q��� $��img/alarm-dark.png��$��8��$��8��$��520,384,272,74q��� -   $ imgHyperlinkPro��$��img/hyperlink.png��$��8��$��8��$��744,104,48,354q��� -    ��Name$ tbkFirstName��$��Right=�$��
0,128,48,0q�$	��Top=�
�������$��Nobile)�$��10(�$	��100�� �� TextAlignment$ Right=�����  �� Path$ vm.Profile.FirstName� �� UpdateSourceTrigger$ PropertyChanged=��� -   $ tbkLastName��$��-��$��Right=�$��
0,160,48,0q�$	��Top=�
�������$��Nobile)�$��10(�$	��100��$ Right=��� -    $ tbkBirthdate��$��Right=�$��
0,192,48,0q�$	��Top=�
�������$��Nobile)�$	��100��$��10(����� $ vm.Profile.Birthdate�$ PropertyChanged=��� -!   $ tbkPostalCode��$��Right=�$��
0,224,48,0q�$	��Top=�
�������$��Nobile)�$��10(�$	��100��$ Right=����� $ vm.Profile.PostalCode�$ PropertyChanged=��� -"   $ 	tbkGender��$��Right=�$��
0,256,48,0q�$	��Top=�
�������$��Nobile)�$��10(�$	��100��$ Right=����� $ vm.Profile.Gender�$ PropertyChanged=��� -#   $ 	tbkHeight��$��Right=�$��
0,288,48,0q�$	��Top=�
�������$��Nobile)�$��10(�$	��100��$ Right=����� $ vm.Profile.Height�$ PropertyChanged=��� -$   $ 	tbkWeight��$��Right=�$��
0,320,48,0q�$	��Top=�
�������$��Nobile)�$��10(�$	��100��$ Right=����� $ vm.Profile.Weight�$ PropertyChanged=��� -%   $ 	tbkLocale��$��Right=�$��
0,352,48,0q�$	��Top=�
�������$��Nobile)�$��10(�$	��100��$ Right=����� $  vm.Profile.PreferredLocale�$ PropertyChanged=��� -&   $ tbkLastUpdated��$��Right=�$��
0,384,48,0q�$	��Top=�
�������$��Nobile)�$��10(�$	��100��$ Right=����� $ vm.Profile.LastUpdateTime�$ PropertyChanged=�� -'   $ grdSummaries��$y�Hidden=�$ 0Q�.+��� -(   $ imgCloseSum��$��img/close-red.png��$��16��$��16��$��237,251,547,199q��� -)    ��Name$ 	cnvVisual��$
��Left=�$	��171��$��265,253,0,0q�$	��Top=�$	��503���� 
���   �$
��Left=�$	��162��$��752,253,0,0q�
���   �$	��Top=�$��48���� 
���   �$
��Left=�$	��171��$��265,253,0,0q�
���   �$	��Top=�$��23���� -*   ���	#FFF4F4F5$
��Left=�$��24��
���   �$	��Top=�$	��754��$��0��$ 0Q��� -+   $ imgClose��$��img/close.png��$��8��$��8��$��780,12,12,446q��� -,   $ imgMinimize��$��img/minimize.png��$��8��$��8��$��764,12,28,446q�