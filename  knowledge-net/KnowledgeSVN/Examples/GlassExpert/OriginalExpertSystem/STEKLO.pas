// Модуль, сгенерированный из набора правил
//*****************************************
unit Steklo;

interface
uses
Windows,cp_ie,cp_trs;

   procedure cmp_f1(cmp_nmbc: Word);forward;
   function _steklo: BOOL;forward;
var
reas,vlagn,sodg,zerns,kach,odnor,
polos,vspen,nalser,osv:integer;
implementation
{$R steklo.RES}

function _steklo: BOOL;
begin
  Result:= cmp_pie(nil,@cmp_f1);
end;

procedure cmp_f1(cmp_nmbc: Word);
begin
  {блок работы с конструкциями Pascal;}
  case cmp_nmbc of 
  1: begin
if esSetReasonMode=1 then CMPR;
     end;
  2: begin
 CMP_RET_INT(vlagn);
     end;
  3: begin
 CMP_RET_INT(sodg);
     end;
  4: begin
 CMP_RET_INT(zerns);
     end;
  5: begin
kach:=CMP_UT_INT(1);
     end;
  6: begin
kach:=CMP_UT_INT(1);
     end;
  7: begin
kach:=CMP_UT_INT(1);
     end;
  8: begin
kach:=CMP_UT_INT(1);
     end;
  9: begin
nalser:=CMP_UT_INT(9);
     end;
  10: begin
odnor:=CMP_UT_INT(6);
     end;
  11: begin
odnor:=CMP_UT_INT(6);
     end;
  12: begin
odnor:=CMP_UT_INT(6);
     end;
  13: begin
polos:=CMP_UT_INT(8);
     end;
  14: begin
polos:=CMP_UT_INT(8);
     end;
  15: begin
polos:=CMP_UT_INT(8);
     end;
  16: begin
odnor:=CMP_UT_INT(6);
     end;
  17: begin
osv:=CMP_UT_INT(3);
     end;
  18: begin
osv:=CMP_UT_INT(3);
     end;
  19: begin
osv:=CMP_UT_INT(3);
     end;
  20: begin
vspen:=CMP_UT_INT(4);
     end;
  21: begin
vspen:=CMP_UT_INT(4);
     end;
  22: begin
vspen:=CMP_UT_INT(4);
     end;
  23: begin
polos:=CMP_UT_INT(8);
     end;
  24: begin
polos:=CMP_UT_INT(8);
     end;
  25: begin
nalser:=CMP_UT_INT(9);
     end;
  26: begin
nalser:=CMP_UT_INT(9);
     end;

  end; // endcase 
 end; // endproc
initialization
reas:=0;
kach:=0;
odnor:=0;
polos:=0;
vspen:=0;
nalser:=0;
osv:=0;

end.