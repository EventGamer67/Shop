   M S B A M L   `   `   `  ����?  ;Shop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Shop.WindowsShop.Windows  5      -clr-namespace:Shop.ControlsShop.Controls  5      D @Emoji.Wpf, Version=0.3.4.0, Culture=neutral, PublicKeyToken=null8*clr-namespace:Emoji.Wpf;assembly=Emoji.Wpf	Emoji.Wpf 5          Shop.Windows.SearchPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Shop.Windows     5      0controlsclr-namespace:Shop.Controls     5      D @ModernWpf, Version=0.9.6.0, Culture=neutral, PublicKeyToken=nullM IModernWpf.Controls, Version=0.9.6.0, Culture=neutral, PublicKeyToken=null,ui!http://schemas.modernwpf.com/2019   5
      8emoji*clr-namespace:Emoji.Wpf;assembly=Emoji.Wpf   5      -   5          N� Title$  
SearchPage��5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   � $��0��6   .+�5      �� $��0,0,0,1q�6   ��5      �� 5       �� Color$ White��6&   -   5        AccentColorBrush��5   4   5      9� 5      $��0��6   $��Center=�6$   $��Center=�6H    � UseLayoutRounding .  $��RightToLeft=�6�   $��Center=�6�   $��Center=�6�   ��5      )  #ModernWpf.Controls.SimpleStackPanel  5         Orientation$ 
Horizontal=�6*   $��LeftToRight=�6C   .+�5      *  $ModernWpf.Controls.ToggleSplitButton  -   5          Name$ SortingButton��5   /   .��5      #  ModernWpf.Controls.SymbolIcon  -   5          Name$ mySymbolIcon��5   ,      Symbol$
 List=�6B   $  ModernWpf.Controls.SplitButton   Flyout 5        ModernWpf.Controls.Flyout  5   "   .  (ModernWpf.Controls.Primitives.FlyoutBase	   	Placement$	 Bottom=�6,   
   Content.
 5    &     $ 
Horizontal=�6:      Spacing$ 12Q�6S   .+�5!   *   �� -   5        $��Цене��5!   1   6@   �� 5"   *   -   5        $��
Имени��5"   1   6A   5#   '   5$   #   5&      '  !ModernWpf.Controls.AutoSuggestBox  5*      -   5        $��2��5*   ,   $��3��6<   $��	10,5,10,5q�6Q      	QueryIcon-  'ModernWpf.Controls.IconElementConverter$
 Find 6d   4	  .System.Windows.Automation.AutomationProperties 	  Name$ Search��6u   $��Center=�6�   $	��300��6�   6�   �� 5+      -   5         ��Name$ Filters��5+   #   $��5��64   $��2��6D   $��	10,5,10,5q�6X   $��Center=�6k   $��Center=�6�   $	��200��6�   6�   5,      ��5.      �� 5/      $��4q�6   $!��This Button is left aligned��6(   51      52      � 53   
   $��1��6   .+�54      %
  ModernWpf.Controls.ProgressRing
  -	   5         
 Name$ Loading��54      $	��100��6/   $	��100��6;    
  IsActive . 6H   '  !ModernWpf.Controls.ScrollViewerEx  55       �� PanningMode$ VerticalOnly=�6    .��56      � -
   5         �Name$ ItemsContainer��56       � Orientation$ 
Horizontal=�64   $��LeftToRight=�6M   $��Center=�6i   $��2��6�   57      58      59      5:      5;      