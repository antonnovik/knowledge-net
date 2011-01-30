unit steklo1;

interface

uses
  Windows, Messages, SysUtils, Classes, Graphics, Controls, Forms, Dialogs,
  StdCtrls, ExtCtrls, cp_ie {cp_iesm для small версии};

type
  TForm1 = class(TForm)
    Panel1: TPanel;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    StaticText1: TStaticText;
    StaticText2: TStaticText;
    StaticText3: TStaticText;
    StaticText4: TStaticText;
    Panel2: TPanel;
    StaticText5: TStaticText;
    ST6: TStaticText;
    Button1: TButton;
    Button2: TButton;
    StaticText6: TStaticText;
    CBox1: TCheckBox;
    CBox2: TCheckBox;
    CBox3: TCheckBox;
    Panel3: TPanel;
    RG1: TRadioGroup;
    RG2: TRadioGroup;
    RG3: TRadioGroup;
    procedure Button2Click(Sender: TObject);
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
 t_nalser: array[0..1]of PChar=('отсутствуют','присутствуют');
 t_vspen: array[0..3]of Pchar=(' ','отсутствует','малое','сильное');
 t_odnor: array[0..3]of Pchar=(' ','однородная шихта',
       'неоднородная шихта','очень неоднородная');
 t_polos: array[0..3]of Pchar=(' ','нет полостности',
          'малая','значительная');
 t_kach: array[0..2]of Pchar=(' ','              СТЕКЛО КАЧЕСТВЕННОЕ','              СТЕКЛО НЕ КАЧЕСТВЕННОЕ');
 t_osv: array[0..3]of Pchar=('  ','плохое','удовлетворительное','хорошее');
implementation
uses steklo;
{$R *.DFM}

procedure TForm1.Button2Click(Sender: TObject);
begin
     Close;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
    Label1.Caption:=' ';
    Label2.Caption:=' ';
    Label3.Caption:=' ';
    Label4.Caption:=' ';
    Label5.Caption:=' ';
    ST6.Caption:=' ';
    kach:=0;
    odnor:=0;
    polos:=0;
    vspen:=0;
    nalser:=0;
    osv:=0;
    //***********************
    zerns:=RG1.ItemIndex+1;
    vlagn:=RG2.ItemIndex+1;
    sodg:=RG3.ItemIndex+1;
    if CBox1.Checked then esSetReasonMode:=1
    else esSetReasonMode:=0;
    if CBox2.Checked then esSetTrassMode:=1
    else esSetTrassMode:=0;
    if CBox3.Checked then
        esSetTrassMode:=esSetTrassMode+2;
    _steklo; // вызов консультации !!!
    //*******************************************
    Label1.Caption:=t_nalser[nalser];
    if nalser=0 then Label1.Font.Color:=clBlack
    else Label1.Font.Color:=clRed;
//*******************************************
    Label2.Caption:=t_polos[polos];
    if polos=1 then Label2.Font.Color:=clBlack
    else Label2.Font.Color:=clRed;
//*******************************************
    Label3.Caption:=t_osv[osv];
    if osv=3 then Label3.Font.Color:=clBlack
    else Label3.Font.Color:=clRed;
//*******************************************
    Label4.Caption:=t_vspen[vspen];
    if (vspen=1) or (vspen=2) then Label4.Font.Color:=clBlack
    else Label4.Font.Color:=clRed;
//*******************************************
    Label5.Caption:=t_odnor[odnor];
    if odnor=1 then Label5.Font.Color:=clBlack
    else Label5.Font.Color:=clRed;
//*******************************************
    ST6.Caption:=t_kach[kach];
    if kach=1 then ST6.Font.Color:=clBlack
    else ST6.Font.Color:=clRed;
end;
//---------------------------------------------------------------------------

end.
