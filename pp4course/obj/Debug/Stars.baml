   M S B A M L   `   `   `  ????D  @pp4course, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:pp4course	pp4course  5          pp4course.Stars   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      >My/http://schemas.componentone.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:pp4course     5        N? Title$  Stars??5      .??5      ? 
???????$??0q?6    ? RowDefinitions 5   
   ?? 5      $	??18* ?6   ?? 5      $	??32* ?6   ?? 5      $	??24* ?6   ?? 5      $	??18* ?6   ?? 5      $	??34* ?6   ?? 5      $	??48* ?6   ?? 5      $	??48* ?6   ?? 5      $	??23* ?6   ?? 5      $	??28* ?6   ?? 5      $	??32* ?6   ?? 5      $	??28* ?6   ?? 5      $	??32* ?6   ?? 5      $	??28* ?6   ?? 5      $	??32* ?6   ?? 5      $	??49* ?6    ? ColumnDefinitions 5   
   ?? 5       $	??14* ?6   ?? 5!      $
??110* ?6   ?? 5"      $
??130* ?6   ?? 5#      $
??143* ?6   ?? 5$      $	??78* ?6   ?? 5%      $	??19* ?6   ?? 5&      $	??64* ?6   ?? 5'      $	??78* ?6   ?? 5(      $
??110* ?6   ?? 5*      $	??20* ?6   .+?5-   
   &   System.Windows.Controls.DataGrid  -   5        $??dg_stars??5-      $??2??6#   $??8??60   $??5??6D      
IsReadOnly . $??1??6i   ?? 5.   
   -   5         ??Name$ btn_back??5.      $??8??6#   $??1??63   $??
Назад??6A   $??5q?6i   ?? 5/   
   -   5        $ btn_add??5/      $??4??6"   $??1??62   $??Добавить??6@   $??5q?6j   ?? 50   
   -   5        $ btn_upd??50      $??5??6"   $??1??62   $??Изменить??6@   $??2??6j   $??5q?6~   ?? 51   
   -   5        $ btn_del??51      $??7??6"   $??1??62   $??Удалить??6@   $??5q?6i   ?? 52   
   -   5         ??Name$ tb_src??52      $??1??6"   $??2??6/   $??2??6?   $	??5,0q?6S   6`   ?? 53   
   $??Поиск:??6   $??1??6!   $??1??6.   $??Right=?6>   ?? 54   
   $#??Название звезды??6   $??8??6*   $??2??67   $??1??6L   ?? 55   
   -   5        $ tb_name??55      $??9??6#   $??2??61   $??1??6F   $	??5,0q?6V    ?? 	MaxLength$ 50??6c   ?? 56   
   $??
Масса??6   $??8??6    $??3??6-   ?? 57   
   -   5        $ tb_mass??57      $??9??6#   $??3??61   $??3??6F   $	??5,0q?6V   $ 50??6c   ?? 58   
   $??
Класс??6   $??8??6    $??7??6-   ?? 59   
   -	   5        $??cb_class??59      $??7??6#   $??9??63   ?? 5:   
   $??Радиус??6   $??1??6!   $??10??61   ?? 5;   
   $??Светимость??6   $??10??6%   $??3??63   ?? 5<   
   -
   5        $ 	tb_radius??5<      $??11??6%   $??1??63   $??2??6C   $	??5,0q?6W   $ 50??6d   ?? 5=   
   -   5        $ tb_brightness??5=      $??11??6)   $??3??67   $??3??6G   $	??5,0q?6[   $ 50??6h   ?? 5>   
   $??Магнетизм??6   $??12??6$   $??1??62   ?? 5?   
   -   5        $ tb_magnetization??5?      $??13??6,   $??1??6:   $??2??6J   $	??5,0q?6^   $ 50??6k   ?? 5@   
   -   5        $ btn_exp??5@      $??5??6"   $??13??62   $??Экспорт??6A   $??2??6U   $??5q?6i   6t   ?? 5A   
   -   5        $ 	btn_graph??5A      $??7??6$   $??13??64   $??Графики??6C   $??5q?6W   6b   5B      5D      