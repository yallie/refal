using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Irony.Parsing;
using Irony.Samples;

namespace Irony.Tests
{
	[TestClass]
	public class RefalRegressionTests
	{
		public TestContext TestContext { get; set; }

[TestMethod]
public void RefalTest_Hello() { RunSampleAndCompareResults(

// Refal program
@"$ENTRY Go { =
	<Prout 'Hello'>;
}",

// output
"Hello\r\n");}

[TestMethod]
public void RefalTest_Binary() { RunSampleAndCompareResults(

// Refal program
@"* Adds two binary numbers
* Written by Y [14-06-03]

$ENTRY Go { = 
  <Prout `0 + 0 = ` <AddB ('0') ('0')>>
  <Prout `0 + 1 = ` <AddB ('0') ('1')>>
  <Prout `1 + 0 = ` <AddB ('1') ('0')>>
  <Prout `1 + 1 = ` <AddB ('1') ('1')>>
  <Prout `10 + 01 = ` <AddB ('10') ('01')>>

* 11 + 6 = 17
  <Prout `1011 + 0110 = ` <AddB ('1011') ('0110')>> 

* 353 + 181 = 534
  <Prout `101100001 + 10110101 = ` <AddB ('101100001') ('10110101')>>

* error
  <Prout `asdbn + ddd = ` <AddB ('asdbn') ('ddd')>>
}

AddB {
  () (e.1) = e.1;
  (e.1) () = e.1;
  (e.1 '0') (e.2 s.3) = <AddB (e.1) (e.2)> s.3;
  (e.1 s.3) (e.2 '0') = <AddB (e.1) (e.2)> s.3;
  (e.1 '1') (e.2 '1') = <AddB ('1') (<AddB (e.1) (e.2)>)> '0';

* handle bad input
  e.1 = '** error! **';
}".Replace("`", "\""), 

// Output
@"0 + 0 = 0
0 + 1 = 1
1 + 0 = 1
1 + 1 = 10
10 + 01 = 11
1011 + 0110 = 10001
101100001 + 10110101 = 1000010110
asdbn + ddd = ** error! **
"); }

[TestMethod]
public void RefalTest_Factorial() { RunSampleAndCompareResults(

// Refal program
@"* Factorial *

$ENTRY Go { =
  <Prout <Fact 6>>;
}

Fact {
  0 = 1;
  s.N = <* s.N <Fact <- s.N 1>>>;
}",

// Output
"720\r\n"); }

[TestMethod]
public void RefalTest_ChangeV1() { RunSampleAndCompareResults(

// Refal program
@"/* Change plus to minus */

$ENTRY Go {
	= <Prout <Chpm '++312a=-3+=-'>>;
}

Chpm {
	'+' e.1 = '-' <Chpm e.1>;
	s.1 e.2 = s.1 <Chpm e.2>;
	= ; 
}",

// Output
"--312a=-3-=-\r\n"); }

[TestMethod]
public void RefalTest_ChangeV2() { RunSampleAndCompareResults(

// Refal program
@"/* Change plus to minus, take2 */

$ENTRY Go {
	= <Prout <Chpm '++312a=-3+=-'>>;
}

Chpm {
	e.1 '+' e.2 = e.1 '-' <Chpm e.2>;
	e.1 = e.1;
}",

// Output
"--312a=-3-=-\r\n"); }

[TestMethod]
public void RefalTest_Italian() { RunSampleAndCompareResults(

// Refal program
@"* Translation from Italian into English *

$ENTRY Go { =
   <Prout <Trans-line 'cavallo gatto rana topo gatto porco cane'>>
}
 
* Translate one line
Trans-line {
   ' ' e.X = <Trans-line e.X>; 
   e.Word ' ' e.Rest = <Trans (e.Word) <Table>> ' ' <Trans-line e.Rest>;
   =;
   e.Word = <Trans (e.Word) <Table>>' ';
}
 
* Italian-English dictionary table
Table { = 
   (('cane') 'dog')
   (('gatto') 'cat')
   (('cavallo') 'horse')
   (('rana') 'frog')
   (('porco') 'pig') 
} 

* Translate Italian word into English by table
Trans { 
   (e.It)e.1((e.It)e.Eng)e.2 = e.Eng;
   (e.It)e.1 = '***'; /* word not in table */
}",

// Output
"horse cat frog *** cat pig dog \r\n"); }

[TestMethod]
public void RefalTest_Palyndrome() { RunSampleAndCompareResults(

// Refal program
@"* Palyndrome *

$ENTRY Go { =
  <Prout <Pal 'sator arepo tenet opera rotas'>>
  <Prout <Pal 'quick brown fox'>>
  <Prout <Pal 1 2 3 2 1>>;
}

Pal {
  = True;
  s.1 = True;
  s.1 e.1 s.1 = <Pal e.1>;
  e.1 = False;
}",

// Output
@"True
False
True
");}

[TestMethod]
public void RefalTest_QuinePlain() { 

// Refal program
var program =
@"/* yallie@yandex.ru */ P{e.1 = <Prout e.1>} C{s.1=<Chr s.1>} $ENTRY Go{=<T
('/* yallie@yandex.ru */ P{e.1 = <Prout e.1>} C{s.1=<Chr s.1>} $ENTRY Go{=<T')
('>}T{(e.1)(e.2)=<P e.1><P <T e.1>><P <T e.2>><P e.2>;e.1=(<C 39>e.1<C 39>)}')
>}T{(e.1)(e.2)=<P e.1><P <T e.1>><P <T e.2>><P e.2>;e.1=(<C 39>e.1<C 39>)}
";

RunSampleAndCompareResults(program, program); }

[TestMethod]
public void RefalTest_QuineSimple() { 

// Refal program
var program =
@"* Quine (simple version)
* Written by Alexey Yakovlev <yallie@yandex.ru>

$ENTRY Go { =
  <PrintProgram 1>
  <PrintData>;
}

PrintProgram {
  24 =;
  s.1 = <Prout <Data s.1>><PrintProgram <+ (s.1) 1>>;
}

PrintData { =
  <Prout `Data {`><PrintData 1>;
  24 = <Prout `}`>;
  s.1 = <Prout <WrapData s.1>><PrintData <+ (s.1) 1>>;
}

WrapData {
  s.1 = s.1 ` = ` <Chr 39> <Data s.1> <Chr 39> `;`;
}

Data {
1 = '* Quine (simple version)';
2 = '* Written by Alexey Yakovlev <yallie@yandex.ru>';
3 = '';
4 = '$ENTRY Go { =';
5 = '  <PrintProgram 1>';
6 = '  <PrintData>;';
7 = '}';
8 = '';
9 = 'PrintProgram {';
10 = '  24 =;';
11 = '  s.1 = <Prout <Data s.1>><PrintProgram <+ (s.1) 1>>;';
12 = '}';
13 = '';
14 = 'PrintData { =';
15 = '  <Prout `Data {`><PrintData 1>;';
16 = '  24 = <Prout `}`>;';
17 = '  s.1 = <Prout <WrapData s.1>><PrintData <+ (s.1) 1>>;';
18 = '}';
19 = '';
20 = 'WrapData {';
21 = '  s.1 = s.1 ` = ` <Chr 39> <Data s.1> <Chr 39> `;`;';
22 = '}';
23 = '';
}
".Replace("`", "\"");

RunSampleAndCompareResults(program, program); }

[TestMethod]
public void RefalTest_QuineXplained() { 

// Refal program
var program =
@"* Quine explained
* Written by Alexey Yakovlev <yallie@yandex.ru>

* InsideOut expands (e.1)(e.2) into e.1(e.1)(e.2)e.2
* Quote adds quotes and braces around an expression
* NewLine converts percents into new lines char by char

$ENTRY Go { =
 <Prout
  <InsideOut
   ('* Quine explained%* Written by Alexey Yakovlev <yallie@yandex.ru>%%* InsideOut expands (e.1)(e.2) into e.1(e.1)(e.2)e.2%* Quote adds quotes and braces around an expression%* NewLine converts percents into new lines char by char%%$ENTRY Go { =% <Prout%  <InsideOut%   ')('%  >% >%}%%InsideOut {%  (e.1)(e.2) = <NewLine e.1><Quote e.1><Quote e.2><NewLine e.2>;%}%%Quote {%  e.1 = (<Chr 39>e.1<Chr 39>)%}%%NewLine {%  s.1 e.2, <Chr 37>: s.1 = <Chr 13><Chr 10><NewLine e.2>;%  s.1 e.2 = s.1 <NewLine e.2>;%  =;%}')
  >
 >
}

InsideOut {
  (e.1)(e.2) = <NewLine e.1><Quote e.1><Quote e.2><NewLine e.2>;
}

Quote {
  e.1 = (<Chr 39>e.1<Chr 39>)
}

NewLine {
  s.1 e.2, <Chr 37>: s.1 = <Chr 13><Chr 10><NewLine e.2>;
  s.1 e.2 = s.1 <NewLine e.2>;
  =;
}
";

RunSampleAndCompareResults(program, program); }

[TestMethod]
public void RefalTest_XtrasBigint() { RunSampleAndCompareResults(

// Refal program
@"* BigInteger arithmetics demonstration
* Written by Y [07-02-10]

$ENTRY Go { = 
  <Prout /* 50! / 49! = 50 */
    </
      30414093201713378043612608166064768844377641568960512000000000000
      608281864034267560872252163321295376887552831379210240000000000>>
}",

// Output
"50\r\n"); }

[TestMethod]
public void RefalTest_XtrasFactorial() { RunSampleAndCompareResults(

// Refal program
@"* Factorial using BigInteger arithmetics
* Written by Y [07-02-10]

$ENTRY Go { =
  <Prout <Fact 500>>;
}

Fact {
  0 = 1;
  s.N = <* s.N <Fact <- s.N 1>>>;
}",

// Output
"1220136825991110068701238785423046926253574342803192842192413588385845373153881997605496447502203281863013616477148203584163378722078177200480785205159329285477907571939330603772960859086270429174547882424912726344305670173270769461062802310452644218878789465754777149863494367781037644274033827365397471386477878495438489595537537990423241061271326984327745715546309977202781014561081188373709531016356324432987029563896628911658974769572087926928871281780070265174507768410719624390394322536422605234945850129918571501248706961568141625359056693423813008856249246891564126775654481886506593847951775360894005745238940335798476363944905313062323749066445048824665075946735862074637925184200459369692981022263971952597190945217823331756934581508552332820762820023402626907898342451712006207714640979456116127629145951237229913340169552363850942885592018727433795173014586357570828355780158735432768888680120399882384702151467605445407663535984174430480128938313896881639487469658817504506926365338175055478128640000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000\r\n"); }

[TestMethod]
public void RefalTest_99BottlesV1() { RunSampleAndCompareResults(

// Refal program
@"* http://99-bottles-of-beer.net/language-refal-1725.html
* Changed \n to \r\n
* -----------------------------------

* Refal is `Recursive functions algorithmic language`
* and is a pattern matching programming language.
* This entry uses both recursion and pattern matching
* capabilities of refal.

$ENTRY Go { = <Sing 99>;};

Sing {
    s.N = <Prout <Verse s.N>>;
};

Beer {
    0 = `No more bottles of beer`;
    1 = `One bottle of beer`;
    s.N = s.N ` bottles of beer`;
};

Verse {
   0 = ``;
   s.N = <Beer s.N> ` on the wall.\r\n`
        `Take one down, pass it around,\r\n`
        <Beer <- s.N 1>> ` on the wall\r\n\r\n`
        <Verse <- s.N 1>>;
};".Replace("`", "\""),

// Output
@"99 bottles of beer on the wall.
Take one down, pass it around,
98 bottles of beer on the wall

98 bottles of beer on the wall.
Take one down, pass it around,
97 bottles of beer on the wall

97 bottles of beer on the wall.
Take one down, pass it around,
96 bottles of beer on the wall

96 bottles of beer on the wall.
Take one down, pass it around,
95 bottles of beer on the wall

95 bottles of beer on the wall.
Take one down, pass it around,
94 bottles of beer on the wall

94 bottles of beer on the wall.
Take one down, pass it around,
93 bottles of beer on the wall

93 bottles of beer on the wall.
Take one down, pass it around,
92 bottles of beer on the wall

92 bottles of beer on the wall.
Take one down, pass it around,
91 bottles of beer on the wall

91 bottles of beer on the wall.
Take one down, pass it around,
90 bottles of beer on the wall

90 bottles of beer on the wall.
Take one down, pass it around,
89 bottles of beer on the wall

89 bottles of beer on the wall.
Take one down, pass it around,
88 bottles of beer on the wall

88 bottles of beer on the wall.
Take one down, pass it around,
87 bottles of beer on the wall

87 bottles of beer on the wall.
Take one down, pass it around,
86 bottles of beer on the wall

86 bottles of beer on the wall.
Take one down, pass it around,
85 bottles of beer on the wall

85 bottles of beer on the wall.
Take one down, pass it around,
84 bottles of beer on the wall

84 bottles of beer on the wall.
Take one down, pass it around,
83 bottles of beer on the wall

83 bottles of beer on the wall.
Take one down, pass it around,
82 bottles of beer on the wall

82 bottles of beer on the wall.
Take one down, pass it around,
81 bottles of beer on the wall

81 bottles of beer on the wall.
Take one down, pass it around,
80 bottles of beer on the wall

80 bottles of beer on the wall.
Take one down, pass it around,
79 bottles of beer on the wall

79 bottles of beer on the wall.
Take one down, pass it around,
78 bottles of beer on the wall

78 bottles of beer on the wall.
Take one down, pass it around,
77 bottles of beer on the wall

77 bottles of beer on the wall.
Take one down, pass it around,
76 bottles of beer on the wall

76 bottles of beer on the wall.
Take one down, pass it around,
75 bottles of beer on the wall

75 bottles of beer on the wall.
Take one down, pass it around,
74 bottles of beer on the wall

74 bottles of beer on the wall.
Take one down, pass it around,
73 bottles of beer on the wall

73 bottles of beer on the wall.
Take one down, pass it around,
72 bottles of beer on the wall

72 bottles of beer on the wall.
Take one down, pass it around,
71 bottles of beer on the wall

71 bottles of beer on the wall.
Take one down, pass it around,
70 bottles of beer on the wall

70 bottles of beer on the wall.
Take one down, pass it around,
69 bottles of beer on the wall

69 bottles of beer on the wall.
Take one down, pass it around,
68 bottles of beer on the wall

68 bottles of beer on the wall.
Take one down, pass it around,
67 bottles of beer on the wall

67 bottles of beer on the wall.
Take one down, pass it around,
66 bottles of beer on the wall

66 bottles of beer on the wall.
Take one down, pass it around,
65 bottles of beer on the wall

65 bottles of beer on the wall.
Take one down, pass it around,
64 bottles of beer on the wall

64 bottles of beer on the wall.
Take one down, pass it around,
63 bottles of beer on the wall

63 bottles of beer on the wall.
Take one down, pass it around,
62 bottles of beer on the wall

62 bottles of beer on the wall.
Take one down, pass it around,
61 bottles of beer on the wall

61 bottles of beer on the wall.
Take one down, pass it around,
60 bottles of beer on the wall

60 bottles of beer on the wall.
Take one down, pass it around,
59 bottles of beer on the wall

59 bottles of beer on the wall.
Take one down, pass it around,
58 bottles of beer on the wall

58 bottles of beer on the wall.
Take one down, pass it around,
57 bottles of beer on the wall

57 bottles of beer on the wall.
Take one down, pass it around,
56 bottles of beer on the wall

56 bottles of beer on the wall.
Take one down, pass it around,
55 bottles of beer on the wall

55 bottles of beer on the wall.
Take one down, pass it around,
54 bottles of beer on the wall

54 bottles of beer on the wall.
Take one down, pass it around,
53 bottles of beer on the wall

53 bottles of beer on the wall.
Take one down, pass it around,
52 bottles of beer on the wall

52 bottles of beer on the wall.
Take one down, pass it around,
51 bottles of beer on the wall

51 bottles of beer on the wall.
Take one down, pass it around,
50 bottles of beer on the wall

50 bottles of beer on the wall.
Take one down, pass it around,
49 bottles of beer on the wall

49 bottles of beer on the wall.
Take one down, pass it around,
48 bottles of beer on the wall

48 bottles of beer on the wall.
Take one down, pass it around,
47 bottles of beer on the wall

47 bottles of beer on the wall.
Take one down, pass it around,
46 bottles of beer on the wall

46 bottles of beer on the wall.
Take one down, pass it around,
45 bottles of beer on the wall

45 bottles of beer on the wall.
Take one down, pass it around,
44 bottles of beer on the wall

44 bottles of beer on the wall.
Take one down, pass it around,
43 bottles of beer on the wall

43 bottles of beer on the wall.
Take one down, pass it around,
42 bottles of beer on the wall

42 bottles of beer on the wall.
Take one down, pass it around,
41 bottles of beer on the wall

41 bottles of beer on the wall.
Take one down, pass it around,
40 bottles of beer on the wall

40 bottles of beer on the wall.
Take one down, pass it around,
39 bottles of beer on the wall

39 bottles of beer on the wall.
Take one down, pass it around,
38 bottles of beer on the wall

38 bottles of beer on the wall.
Take one down, pass it around,
37 bottles of beer on the wall

37 bottles of beer on the wall.
Take one down, pass it around,
36 bottles of beer on the wall

36 bottles of beer on the wall.
Take one down, pass it around,
35 bottles of beer on the wall

35 bottles of beer on the wall.
Take one down, pass it around,
34 bottles of beer on the wall

34 bottles of beer on the wall.
Take one down, pass it around,
33 bottles of beer on the wall

33 bottles of beer on the wall.
Take one down, pass it around,
32 bottles of beer on the wall

32 bottles of beer on the wall.
Take one down, pass it around,
31 bottles of beer on the wall

31 bottles of beer on the wall.
Take one down, pass it around,
30 bottles of beer on the wall

30 bottles of beer on the wall.
Take one down, pass it around,
29 bottles of beer on the wall

29 bottles of beer on the wall.
Take one down, pass it around,
28 bottles of beer on the wall

28 bottles of beer on the wall.
Take one down, pass it around,
27 bottles of beer on the wall

27 bottles of beer on the wall.
Take one down, pass it around,
26 bottles of beer on the wall

26 bottles of beer on the wall.
Take one down, pass it around,
25 bottles of beer on the wall

25 bottles of beer on the wall.
Take one down, pass it around,
24 bottles of beer on the wall

24 bottles of beer on the wall.
Take one down, pass it around,
23 bottles of beer on the wall

23 bottles of beer on the wall.
Take one down, pass it around,
22 bottles of beer on the wall

22 bottles of beer on the wall.
Take one down, pass it around,
21 bottles of beer on the wall

21 bottles of beer on the wall.
Take one down, pass it around,
20 bottles of beer on the wall

20 bottles of beer on the wall.
Take one down, pass it around,
19 bottles of beer on the wall

19 bottles of beer on the wall.
Take one down, pass it around,
18 bottles of beer on the wall

18 bottles of beer on the wall.
Take one down, pass it around,
17 bottles of beer on the wall

17 bottles of beer on the wall.
Take one down, pass it around,
16 bottles of beer on the wall

16 bottles of beer on the wall.
Take one down, pass it around,
15 bottles of beer on the wall

15 bottles of beer on the wall.
Take one down, pass it around,
14 bottles of beer on the wall

14 bottles of beer on the wall.
Take one down, pass it around,
13 bottles of beer on the wall

13 bottles of beer on the wall.
Take one down, pass it around,
12 bottles of beer on the wall

12 bottles of beer on the wall.
Take one down, pass it around,
11 bottles of beer on the wall

11 bottles of beer on the wall.
Take one down, pass it around,
10 bottles of beer on the wall

10 bottles of beer on the wall.
Take one down, pass it around,
9 bottles of beer on the wall

9 bottles of beer on the wall.
Take one down, pass it around,
8 bottles of beer on the wall

8 bottles of beer on the wall.
Take one down, pass it around,
7 bottles of beer on the wall

7 bottles of beer on the wall.
Take one down, pass it around,
6 bottles of beer on the wall

6 bottles of beer on the wall.
Take one down, pass it around,
5 bottles of beer on the wall

5 bottles of beer on the wall.
Take one down, pass it around,
4 bottles of beer on the wall

4 bottles of beer on the wall.
Take one down, pass it around,
3 bottles of beer on the wall

3 bottles of beer on the wall.
Take one down, pass it around,
2 bottles of beer on the wall

2 bottles of beer on the wall.
Take one down, pass it around,
One bottle of beer on the wall

One bottle of beer on the wall.
Take one down, pass it around,
No more bottles of beer on the wall


"); }

[TestMethod]
public void RefalTest_99BottlesV2() { RunSampleAndCompareResults(

// Refal program
@"* http://99-bottles-of-beer.net/language-refal5-491.html
* Removed infinite loop
* -----------------------------------

* Refal5 version of famous lyrics
* Created and debugged carefully by Dendik (ru.pochtamt[at]dendik)
* You can get some info on Refal at http://refal.org/index_e.htm

* Bronikkk: Something like that, maybe?

$ENTRY Go { = <Go 99>; 100=; t.n = <Go <Do t.n>>; };

Do {
  next t.n, <- (t.n) 1> : { -1 = 100; t.m = t.m; };
  look t.n = <Prout <Do Look t.n>>;
  Look t.n = <Do Beer t.n> <Do wall> ', ' <Do beer t.n>;
  drink t.n = <Prout <Do Drink t.n>> <Prout> t.n;
  Drink 100 = 'Go to the store, buy some more, ' <Do beer 99>;
  Drink t.n = 'Take one down, pass it around, ' <Do beer t.n>;
  wall = ' on the wall';
  Beer 0 = 'No more ' <Do beer>;
  Beer 1 = 1 <Do beer1>;
  Beer t.n = t.n <Do beer>;
  beer 0 = 'no more ' <Do beer>;
  beer 1 = 1 <Do beer1>;
  beer t.n = t.n <Do beer>;
  beer = ' bottles of beer';
  beer1 = ' bottle of beer';
  t.n = <Do look t.n> <Do drink <Do next t.n>>;
};",

// Output
@"99 bottles of beer on the wall, 99 bottles of beer
Take one down, pass it around, 98 bottles of beer

98 bottles of beer on the wall, 98 bottles of beer
Take one down, pass it around, 97 bottles of beer

97 bottles of beer on the wall, 97 bottles of beer
Take one down, pass it around, 96 bottles of beer

96 bottles of beer on the wall, 96 bottles of beer
Take one down, pass it around, 95 bottles of beer

95 bottles of beer on the wall, 95 bottles of beer
Take one down, pass it around, 94 bottles of beer

94 bottles of beer on the wall, 94 bottles of beer
Take one down, pass it around, 93 bottles of beer

93 bottles of beer on the wall, 93 bottles of beer
Take one down, pass it around, 92 bottles of beer

92 bottles of beer on the wall, 92 bottles of beer
Take one down, pass it around, 91 bottles of beer

91 bottles of beer on the wall, 91 bottles of beer
Take one down, pass it around, 90 bottles of beer

90 bottles of beer on the wall, 90 bottles of beer
Take one down, pass it around, 89 bottles of beer

89 bottles of beer on the wall, 89 bottles of beer
Take one down, pass it around, 88 bottles of beer

88 bottles of beer on the wall, 88 bottles of beer
Take one down, pass it around, 87 bottles of beer

87 bottles of beer on the wall, 87 bottles of beer
Take one down, pass it around, 86 bottles of beer

86 bottles of beer on the wall, 86 bottles of beer
Take one down, pass it around, 85 bottles of beer

85 bottles of beer on the wall, 85 bottles of beer
Take one down, pass it around, 84 bottles of beer

84 bottles of beer on the wall, 84 bottles of beer
Take one down, pass it around, 83 bottles of beer

83 bottles of beer on the wall, 83 bottles of beer
Take one down, pass it around, 82 bottles of beer

82 bottles of beer on the wall, 82 bottles of beer
Take one down, pass it around, 81 bottles of beer

81 bottles of beer on the wall, 81 bottles of beer
Take one down, pass it around, 80 bottles of beer

80 bottles of beer on the wall, 80 bottles of beer
Take one down, pass it around, 79 bottles of beer

79 bottles of beer on the wall, 79 bottles of beer
Take one down, pass it around, 78 bottles of beer

78 bottles of beer on the wall, 78 bottles of beer
Take one down, pass it around, 77 bottles of beer

77 bottles of beer on the wall, 77 bottles of beer
Take one down, pass it around, 76 bottles of beer

76 bottles of beer on the wall, 76 bottles of beer
Take one down, pass it around, 75 bottles of beer

75 bottles of beer on the wall, 75 bottles of beer
Take one down, pass it around, 74 bottles of beer

74 bottles of beer on the wall, 74 bottles of beer
Take one down, pass it around, 73 bottles of beer

73 bottles of beer on the wall, 73 bottles of beer
Take one down, pass it around, 72 bottles of beer

72 bottles of beer on the wall, 72 bottles of beer
Take one down, pass it around, 71 bottles of beer

71 bottles of beer on the wall, 71 bottles of beer
Take one down, pass it around, 70 bottles of beer

70 bottles of beer on the wall, 70 bottles of beer
Take one down, pass it around, 69 bottles of beer

69 bottles of beer on the wall, 69 bottles of beer
Take one down, pass it around, 68 bottles of beer

68 bottles of beer on the wall, 68 bottles of beer
Take one down, pass it around, 67 bottles of beer

67 bottles of beer on the wall, 67 bottles of beer
Take one down, pass it around, 66 bottles of beer

66 bottles of beer on the wall, 66 bottles of beer
Take one down, pass it around, 65 bottles of beer

65 bottles of beer on the wall, 65 bottles of beer
Take one down, pass it around, 64 bottles of beer

64 bottles of beer on the wall, 64 bottles of beer
Take one down, pass it around, 63 bottles of beer

63 bottles of beer on the wall, 63 bottles of beer
Take one down, pass it around, 62 bottles of beer

62 bottles of beer on the wall, 62 bottles of beer
Take one down, pass it around, 61 bottles of beer

61 bottles of beer on the wall, 61 bottles of beer
Take one down, pass it around, 60 bottles of beer

60 bottles of beer on the wall, 60 bottles of beer
Take one down, pass it around, 59 bottles of beer

59 bottles of beer on the wall, 59 bottles of beer
Take one down, pass it around, 58 bottles of beer

58 bottles of beer on the wall, 58 bottles of beer
Take one down, pass it around, 57 bottles of beer

57 bottles of beer on the wall, 57 bottles of beer
Take one down, pass it around, 56 bottles of beer

56 bottles of beer on the wall, 56 bottles of beer
Take one down, pass it around, 55 bottles of beer

55 bottles of beer on the wall, 55 bottles of beer
Take one down, pass it around, 54 bottles of beer

54 bottles of beer on the wall, 54 bottles of beer
Take one down, pass it around, 53 bottles of beer

53 bottles of beer on the wall, 53 bottles of beer
Take one down, pass it around, 52 bottles of beer

52 bottles of beer on the wall, 52 bottles of beer
Take one down, pass it around, 51 bottles of beer

51 bottles of beer on the wall, 51 bottles of beer
Take one down, pass it around, 50 bottles of beer

50 bottles of beer on the wall, 50 bottles of beer
Take one down, pass it around, 49 bottles of beer

49 bottles of beer on the wall, 49 bottles of beer
Take one down, pass it around, 48 bottles of beer

48 bottles of beer on the wall, 48 bottles of beer
Take one down, pass it around, 47 bottles of beer

47 bottles of beer on the wall, 47 bottles of beer
Take one down, pass it around, 46 bottles of beer

46 bottles of beer on the wall, 46 bottles of beer
Take one down, pass it around, 45 bottles of beer

45 bottles of beer on the wall, 45 bottles of beer
Take one down, pass it around, 44 bottles of beer

44 bottles of beer on the wall, 44 bottles of beer
Take one down, pass it around, 43 bottles of beer

43 bottles of beer on the wall, 43 bottles of beer
Take one down, pass it around, 42 bottles of beer

42 bottles of beer on the wall, 42 bottles of beer
Take one down, pass it around, 41 bottles of beer

41 bottles of beer on the wall, 41 bottles of beer
Take one down, pass it around, 40 bottles of beer

40 bottles of beer on the wall, 40 bottles of beer
Take one down, pass it around, 39 bottles of beer

39 bottles of beer on the wall, 39 bottles of beer
Take one down, pass it around, 38 bottles of beer

38 bottles of beer on the wall, 38 bottles of beer
Take one down, pass it around, 37 bottles of beer

37 bottles of beer on the wall, 37 bottles of beer
Take one down, pass it around, 36 bottles of beer

36 bottles of beer on the wall, 36 bottles of beer
Take one down, pass it around, 35 bottles of beer

35 bottles of beer on the wall, 35 bottles of beer
Take one down, pass it around, 34 bottles of beer

34 bottles of beer on the wall, 34 bottles of beer
Take one down, pass it around, 33 bottles of beer

33 bottles of beer on the wall, 33 bottles of beer
Take one down, pass it around, 32 bottles of beer

32 bottles of beer on the wall, 32 bottles of beer
Take one down, pass it around, 31 bottles of beer

31 bottles of beer on the wall, 31 bottles of beer
Take one down, pass it around, 30 bottles of beer

30 bottles of beer on the wall, 30 bottles of beer
Take one down, pass it around, 29 bottles of beer

29 bottles of beer on the wall, 29 bottles of beer
Take one down, pass it around, 28 bottles of beer

28 bottles of beer on the wall, 28 bottles of beer
Take one down, pass it around, 27 bottles of beer

27 bottles of beer on the wall, 27 bottles of beer
Take one down, pass it around, 26 bottles of beer

26 bottles of beer on the wall, 26 bottles of beer
Take one down, pass it around, 25 bottles of beer

25 bottles of beer on the wall, 25 bottles of beer
Take one down, pass it around, 24 bottles of beer

24 bottles of beer on the wall, 24 bottles of beer
Take one down, pass it around, 23 bottles of beer

23 bottles of beer on the wall, 23 bottles of beer
Take one down, pass it around, 22 bottles of beer

22 bottles of beer on the wall, 22 bottles of beer
Take one down, pass it around, 21 bottles of beer

21 bottles of beer on the wall, 21 bottles of beer
Take one down, pass it around, 20 bottles of beer

20 bottles of beer on the wall, 20 bottles of beer
Take one down, pass it around, 19 bottles of beer

19 bottles of beer on the wall, 19 bottles of beer
Take one down, pass it around, 18 bottles of beer

18 bottles of beer on the wall, 18 bottles of beer
Take one down, pass it around, 17 bottles of beer

17 bottles of beer on the wall, 17 bottles of beer
Take one down, pass it around, 16 bottles of beer

16 bottles of beer on the wall, 16 bottles of beer
Take one down, pass it around, 15 bottles of beer

15 bottles of beer on the wall, 15 bottles of beer
Take one down, pass it around, 14 bottles of beer

14 bottles of beer on the wall, 14 bottles of beer
Take one down, pass it around, 13 bottles of beer

13 bottles of beer on the wall, 13 bottles of beer
Take one down, pass it around, 12 bottles of beer

12 bottles of beer on the wall, 12 bottles of beer
Take one down, pass it around, 11 bottles of beer

11 bottles of beer on the wall, 11 bottles of beer
Take one down, pass it around, 10 bottles of beer

10 bottles of beer on the wall, 10 bottles of beer
Take one down, pass it around, 9 bottles of beer

9 bottles of beer on the wall, 9 bottles of beer
Take one down, pass it around, 8 bottles of beer

8 bottles of beer on the wall, 8 bottles of beer
Take one down, pass it around, 7 bottles of beer

7 bottles of beer on the wall, 7 bottles of beer
Take one down, pass it around, 6 bottles of beer

6 bottles of beer on the wall, 6 bottles of beer
Take one down, pass it around, 5 bottles of beer

5 bottles of beer on the wall, 5 bottles of beer
Take one down, pass it around, 4 bottles of beer

4 bottles of beer on the wall, 4 bottles of beer
Take one down, pass it around, 3 bottles of beer

3 bottles of beer on the wall, 3 bottles of beer
Take one down, pass it around, 2 bottles of beer

2 bottles of beer on the wall, 2 bottles of beer
Take one down, pass it around, 1 bottle of beer

1 bottle of beer on the wall, 1 bottle of beer
Take one down, pass it around, no more  bottles of beer

No more  bottles of beer on the wall, no more  bottles of beer
Go to the store, buy some more, 99 bottles of beer

");}

[TestMethod]
public void RefalTest_Template() { RunSampleAndCompareResults(

// Refal program
@"",

// Output
@""); }

		void RunSampleAndCompareResults(string program, string output)
		{
			var grammar = new RefalGrammar();
			var parser = new Parser(grammar);
			var parseTree = parser.Parse(program);

			Assert.IsNotNull(parseTree);
			Assert.IsFalse(parseTree.HasErrors());

			string result = grammar.RunSample(parseTree);
			Assert.IsNotNull(result);
			Assert.AreEqual(result, output);
		}
	}
}
