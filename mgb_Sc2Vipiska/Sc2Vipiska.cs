// ������ 1.01 �� 13 ������� 2017�. ���������� ������� � ������� ������-2.
// encoding=cp-1251
/*
		��������� ��������� :

	-DateFrom	��������� ���� , � ���� 2017.10.13
	-DateInto	�������� ����
	-BranchId	����� ������
	-UserId		����� ������������
	-GroupId	����� ������ ������

*/
using	__	=	MyTypes.CCommon ;
using	MyTypes;

class	Sc2Vipiska {

	static	void	PrintAboutMe() {
		__.Print(CAbc.EMPTY," ������ 1.01 �� 13 ������� 2017�. ���������� ������� � ������� ������-2.");
		__.Print(CAbc.EMPTY,"\t\t��������� ��������� :",CAbc.EMPTY);
		__.Print("\t-DateFrom\t��������� ���� , � ���� 2017.10.13");
		__.Print("\t-DateInto\t�������� ����");
		__.Print("\t-BranchId\t����� ������");
		__.Print("\t-UserId\t\t����� ������������");
		__.Print("\t-GroupId\t����� ������ ������");
	}

	static	void	Main() {
		const	bool	DEBUG		=	false
		;
		string		ScroogeDir	=	CAbc.EMPTY
		,		ServerName	=	CAbc.EMPTY
		,		DataBase	=	CAbc.EMPTY
		,		ConnectionString=	CAbc.EMPTY
		;
		int		UserId		=	0
		,		GroupId		=	0
		,		BranchId	=	0
		,		DateFrom	=	0
		,		DateInto	=	0
		;
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		CParam		Param		= new	CParam()	;
		if	( ! DEBUG )
			if	( __.ParamCount() < 2 ) {
				PrintAboutMe();
				return;
			}
		if	( ! CCommon.IsEmpty( Param["UserId"] ) )
			UserId		=	__.CInt( Param["UserId"] );
		if	( ! CCommon.IsEmpty( Param["GroupId"] ) )
			UserId		=	__.CInt( Param["GroupId"] );
		if	( ! CCommon.IsEmpty( Param["BranchId"] ) )
			BranchId	=	__.CInt( Param["BranchId"] );
		if	( ! CCommon.IsEmpty( Param["DateInto"] ) )
			DateInto	=	__.GetDate( Param["DateInto"] );
		if	( ! CCommon.IsEmpty( Param["DateFrom"] ) )
			DateFrom	=	CCommon.GetDate( Param["DateFrom"] );
		if	( ( DateFrom == 0 ) && ( DateInto != 0 ) )
			DateFrom	=	DateInto ;
		if	( DateInto == 0 ) {
			__.Print("������ : �� ������ �������� ���� !");
			return;
		}
		if	( ( BranchId == 0 ) && ( UserId == 0 ) && ( GroupId == 0 ) )  {
			__.Print("������ : �� ������ ������� ���������� ������ !");
			return;
		}
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		CConsole.Color	=	CConsole.GRAY;
		CConsole.Clear();
		CCommon.Print( ""," ���������� ������� � ������� ������-2. ������ 1.01 �� 13.10.2017�." ,"") ;
		CScrooge2Config	Scrooge2Config	= new	CScrooge2Config();
		if	(!Scrooge2Config.IsValid) {
			CCommon.Print( Scrooge2Config.ErrInfo ) ;
			return;
		}
		ScroogeDir	=	(string)Scrooge2Config["Root"].Trim();
		ServerName	=	(string)Scrooge2Config["Server"];
		DataBase	=	(string)Scrooge2Config["DataBase"];
		if( ScroogeDir == null ) {
			CCommon.Print("  �� ������� ���������� `Root` � ���������� `������-2` ");
			return;
		}
		if( ServerName == null ) {
			CCommon.Print("  �� ������� ���������� `Server` � ���������� `������-2` ");
			return;
		}
		if( DataBase == null ) {
			CCommon.Print("  �� ������� ���������� `Database` � ���������� `������-2` ");
			return;
		}
		CCommon.Print("  ���� ��������� `������-2` ����� :  " + ScroogeDir );
		__.Print("  ������        :  " + ServerName  );
		__.Print("  ���� ������   :  " + DataBase + CAbc.CRLF );
		ConnectionString	=	"Server="	+	ServerName
					+	";Database="	+	DataBase
					+	";Integrated Security=TRUE;"  ;
		// - - - - - - - - - - - - - - - - - - - - - - - - - - - -
		System.Console.Title="  ������� � ������� ������-2              |   "+ServerName+"."+DataBase	;
		CSc2Extract	Sc2Extract	= new	CSc2Extract()	;
		if	( Sc2Extract.Open( ConnectionString ) ) {
			Sc2Extract.Path		=	__.GetCurDir()	;
			Sc2Extract.DateFrom	=	DateFrom	;
			Sc2Extract.DateInto	=	DateInto	;
			Sc2Extract.CbMode	=	false		;
			Sc2Extract.CoolSum	=	false		;
			Sc2Extract.ApartFile	=	true		;
			Sc2Extract.NeedPrintMsg	=	true		;
			Sc2Extract.OverMode	=	2		;
			Sc2Extract.BranchId	=	BranchId	;
			Sc2Extract.GroupId	=	GroupId		;
			Sc2Extract.UserId	=	UserId		;
			Sc2Extract.Build();
			Sc2Extract.Close();
			__.Print("������� ���������.","��� ����������� ������� Enter...");
			CConsole.ClearKeyboard();
			CConsole.Flash();
			CConsole.ReadChar();
		}
	}
}