## Saityno taikomųjų programų projektavimas 
# Leftovers - nenupirkto maisto svetainė

##	Sistemos paskirtis

Projekto tikslas – suteikti galimybę maisto gamintojams parduoti nenupirktą maistą.

Veikimo principas – kuriamą sistemą sudaro dvi dalys: internetinė aplikacija, kuria naudosis maisto gamintojai, norintys parduoti nenupirktą maistą, vartotojai, siekiantys įsigyti maistą, administratorius bei aplikacijų programavimo sąsaja.

Maisto gamintojas, norėdamas naudotis nenupirkto maisto svetaine „Likučiai“, turės prisiregistruoti prie internetinės svetainės ir galės padaryti įrašą apie nepasiimtą, nenupirktą, pagal klaidingą užsakymą pagamintą maisto prekę ar patiekalą.  Vartotojas, prisiregistravęs prie sistemos, matys visus restoranų pasiūlymus. Vartotojas, radęs jį dominantį pasiūlymą, atliks pirkimą, įrašas bus pašalintas iš sąrašo. Tada vartotojas galės nukeliavęs į įrašę nurodytą adresą atsiimti maistą. Administratorius galės šalinti įrašus, šalinti vartotojų paskyras.

##	Funkciniai reikalavimai
Neregistruotas sistemos naudotojas galės:
-	Peržiūrėti platformos pagrindinį puslapį
-	Prisiregistruoti prie internetinės aplikacijos

Registruotas sistemos naudotojas galės:
- Atsijungti nuo internetinės aplikacijos
- Prisijungti prie svetainės
- Sukurti naują įrašą
- Peržiūrėti įkeltus įrašus
- Atlikti pirkimą

Administratorius galės:
- Šalinti įrašus
- Šalinti vartotojų paskyras

##	Sistemos architektūra

Sistemos sudedamosios dalys:
-	Kliento pusė (ang. Front-End) – naudojant React.js;
-	Serverio pusė (angl. Back-End) – naudojant C# .NET. Duomenų bazė – MySQL. 

1 pav. pavaizduota kuriamos sistemos diegimo diagrama. Sistemos talpinimui yra naudojamas Azure serveris. Kiekviena sistemos dalis yra diegiama tame pačiame serveryje. Internetinė aplikacija yra pasiekiama per HTTPS protokolą. Šios sistemos veikimui yra reikalingas Leftovers API, kuris pasiekiamas per aplikacijų programavimo sąsają. Pats Leftovers API vykdo duomenų mainus su duomenų baze - tam naudojama TCP/IP sąsaja.

![](https://i.im.ge/2022/09/23/1ikzUy.Model.jpg)
