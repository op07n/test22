   M S B A M L   `   `   `  ����K  GPanuon.UI.Silver, Version=1.0.9.5, Culture=neutral, PublicKeyToken=nullM8clr-namespace:Panuon.UI.Silver;assembly=Panuon.UI.SilverPanuon.UI.Silver  5   	   S O疫情查询 - 桌面版, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:疫情查询___桌面版疫情查询___桌面版 5   	   )   #疫情查询___桌面版.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Gpu8clr-namespace:Panuon.UI.Silver;assembly=Panuon.UI.Silver     5   	   8local&clr-namespace:疫情查询___桌面版     5   	   $	��384��5	   	   $��133.577��5
   	   
������   � 
ResizeMode$  NoResize=�5   	    � WindowStyle$
 None=�5   	    � ShowInTaskbar .  .��5      � .+�5   
   �� -   5         ��Name$
 Main��5      .+�5      � .+�5      �� -   5         ��Name$	 GET��5      $��0��6)   $��34��62   $	��Top=�6>    �� TextAlignment$ Right=�6V   $
��20,0q�6l   �� 5      -   5        $
 MGET��5      $��确诊��6*   $��
10,34,10,0q�64   $ Right=�6H   $��28��6^   $	��Top=�6j   5      � 5      .+�5      �� -   5        $	 MAY��5      $��0��6)   $��34��62   $	��Top=�6>   $ Right=�6V   $
��20,0q�6l   �� 5      -   5        $
 MMAY��5      $��疑似��6*   $��
10,34,10,0q�64   $ Right=�6H   $��28��6^   $	��Top=�6j   5      � 5      .+�5      �� -   5        $ MAYDIE��5      $��0��6,   $��34��65   $	��Top=�6A   $ Right=�6Y   $
��20,0q�6o   �� 5      -   5        $ MMAYDIE��5      $��重症��6-   $��
10,34,10,0q�67   $ Right=�6K   $��28��6a   $	��Top=�6m   5      � 5      .+�5      �� -   5        $	 DIE��5      $��0��6)   $��34��62   $	��Top=�6>   $ Right=�6V   $
��20,0q�6l   �� 5      -	   5        $
 MDIE��5      $��死亡��6*   $��
10,34,10,0q�64   $ Right=�6H   $��28��6^   $	��Top=�6j   5      � 5       .+�5!      �� -
   5        $ ALIVE��5!      $��0��6+   $��34��64   $	��Top=�6@   $ Right=�6X   $
��20,0q�6n   �� 5"      -   5        $ MALIVE��5"      $��治愈��6,   $��
10,34,10,0q�66   $ Right=�6J   $��28��6`   $	��Top=�6l   5#      � 5$      -   5         �Name$ WP��5$      $	��134��6$   $��RightToLeft=�60   .+�5%      �� -   5         ��Name$	 SET��5%      #   Panuon.UI.Silver.ButtonHelper   ButtonStyle$
 Link=�6&   	   Icon$		 ��6I   $��0q�6b   $��30��6m   $��30��6y   6�   �� 5&      -   5        $	 ERR��5&      $��LeftToRight=�6&   $
 Link=�6B   $��0q�6e   $��30��6p   $��30��6|   
 � ToolTip
 5'      _� 5(      -   5         _�Name$	 TTS��5(   "    _� 	Placement$ Right=�6/   .��5)      �� -   5        $ TOOLTIP��5)   (   ��69   ��  �� Path$ ToolTipText�5*      5,      5-      5.      5/      50      