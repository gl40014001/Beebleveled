   M S B A M L   `   `   `  ����F  BConwaysLife, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:Notadesigner.ConwaysLife.GameNotadesigner.ConwaysLife.Game  5      )    #Notadesigner.ConwaysLife.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   <life+clr-namespace:Notadesigner.ConwaysLife.Game     5      -   5        
�������  � Title$  Level Editor��5   	   $	��575��6   $
��1000��6+   $��Stretch=�68    � Icon$$ /ConwaysLife;component/app.ico��6T   .��5      ]� -   5        $��layout��5      .+�5   
   c� $	��Top=�6   .@�5
      �� -   5        $��btnLoad��5
   $   $
��Open��6   $��23��63   $��75��6?   $��5q�6J   6U   �� 5      -   5        $��
btntstSave��5   $   $
��Save��6   $��23��66   $��75��6B   $��5q�6M   6X   �� 5      -   5        $��btnClear��5   %   $��Clear��6   $��23��65   $��75��6A   $��5q�6L   6W   �� 5      -   5        $��btnUndo��5   $   $
��Undo��6   $��23��63   $��75��6?   $��5q�6J   6U   �� 5      -   5        $��btnRedo��5   $   $
��Redo��6   $��23��63   $��75��6?   $��5q�6J   6U   �� 5      -   5        $��	btnExport��5   &   $��Export��6   $��23��67   $��75��6C   $��5q�6N   6Y   5      �� 5   
   $
��Auto=�6   $
��Auto=�6<   .��5      1   +Notadesigner.ConwaysLife.Game.TilePanelView  -	   5          Name$ tilePanelView��5   !   1   +Notadesigner.ConwaysLife.Game.TilePanelView   Click$ Tpview_Click��6r   $	��Top=�68   $	��700��6M   $	��360��6Z   $��10q�6f   $	��Top=�6�   6�   5      .   (Notadesigner.ConwaysLife.Game.TileEdView  5      -
   5          Name$ 
tileEdView��5      .   (Notadesigner.ConwaysLife.Game.TileEdView   Click$ 
View_Click��6T   $	��Top=�62   $��10q�6H   6i   6   0Notadesigner.ConwaysLife.Game.SelectedColourView  5   
   -   5          Name$ scview��5   "   6   0Notadesigner.ConwaysLife.Game.SelectedColourView   Click$ Scview_Click��6S   $	��Top=�62   $��10q�6G   $��83��6h   6v   /   )Notadesigner.ConwaysLife.Game.PaletteView  5   
   -   5          Name$ paletteview��5      /   )Notadesigner.ConwaysLife.Game.PaletteView	   Click$	 Paletteview_Click��6T   $	��Top=�60   $��10,10q�6E   $��83��6n   6|   5      5      