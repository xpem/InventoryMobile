﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMobile.Utils
{
    public static class SubCategoryIconsList
    {
        public static string GetIconCode(string name) => Icons.Where((s) => s.name == name).First().unicode;

        public static string GetIconName(string unicodde) => Icons.Where((s) => s.unicode == unicodde).First().name;

        /// <summary>
        ///to index icons for his name
        /// </summary>
        private static IEnumerable<(string name, string unicode)> Icons = new List<(string, string)>()
        {
            new ("Ad", "\uf641"),
            new ("AddressBook", "\uf2b9"),
            new ("AddressCard", "\uf2bb"),
            new ("Adjust", "\uf042"),
            new ("AirFreshener", "\uf5d0"),
            new ("AlignCenter", "\uf037"),
            new ("AlignJustify", "\uf039"),
            new ("AlignLeft", "\uf036"),
            new ("AlignRight", "\uf038"),
            new ("Allergies", "\uf461"),
            new ("Ambulance", "\uf0f9"),
            new ("AmericanSignLanguageInterpreting", "\uf2a3"),
            new ("Anchor", "\uf13d"),
            new ("AngleDoubleDown", "\uf103"),
            new ("AngleDoubleLeft", "\uf100"),
            new ("AngleDoubleRight", "\uf101"),
            new ("AngleDoubleUp", "\uf102"),
            new ("AngleDown", "\uf107"),
            new ("AngleLeft", "\uf104"),
            new ("AngleRight", "\uf105"),
            new ("AngleUp", "\uf106"),
            new ("Angry", "\uf556"),
            new ("Ankh", "\uf644"),
            new ("AppleAlt", "\uf5d1"),
            new ("Archive", "\uf187"),
            new ("Archway", "\uf557"),
            new ("ArrowAltCircleDown", "\uf358"),
            new ("ArrowAltCircleLeft", "\uf359"),
            new ("ArrowAltCircleRight", "\uf35a"),
            new ("ArrowAltCircleUp", "\uf35b"),
            new ("ArrowCircleDown", "\uf0ab"),
            new ("ArrowCircleLeft", "\uf0a8"),
            new ("ArrowCircleRight", "\uf0a9"),
            new ("ArrowCircleUp", "\uf0aa"),
            new ("ArrowDown", "\uf063"),
            new ("ArrowLeft", "\uf060"),
            new ("ArrowRight", "\uf061"),
            new ("ArrowUp", "\uf062"),
            new ("ArrowsAltH", "\uf337"),
            new ("ArrowsAltV", "\uf338"),
            new ("ArrowsAlt", "\uf0b2"),
            new ("AssistiveListeningSystems", "\uf2a2"),
            new ("Asterisk", "\uf069"),
            new ("At", "\uf1fa"),
            new ("Atlas", "\uf558"),
            new ("Atom", "\uf5d2"),
            new ("AudioDescription", "\uf29e"),
            new ("Award", "\uf559"),
            new ("BabyCarriage", "\uf77d"),
            new ("Baby", "\uf77c"),
            new ("Backspace", "\uf55a"),
            new ("Backward", "\uf04a"),
            new ("BalanceScale", "\uf24e"),
            new ("Ban", "\uf05e"),
            new ("BandAid", "\uf462"),
            new ("Barcode", "\uf02a"),
            new ("Bars", "\uf0c9"),
            new ("BaseballBall", "\uf433"),
            new ("BasketballBall", "\uf434"),
            new ("Bath", "\uf2cd"),
            new ("BatteryEmpty", "\uf244"),
            new ("BatteryFull", "\uf240"),
            new ("BatteryHalf", "\uf242"),
            new ("BatteryQuarter", "\uf243"),
            new ("BatteryThreeQuarters", "\uf241"),
            new ("Bed", "\uf236"),
            new ("Beer", "\uf0fc"),
            new ("BellSlash", "\uf1f6"),
            new ("Bell", "\uf0f3"),
            new ("BezierCurve", "\uf55b"),
            new ("Bible", "\uf647"),
            new ("Bicycle", "\uf206"),
            new ("Binoculars", "\uf1e5"),
            new ("Biohazard", "\uf780"),
            new ("BirthdayCake", "\uf1fd"),
            new ("BlenderPhone", "\uf6b6"),
            new ("Blender", "\uf517"),
            new ("Blind", "\uf29d"),
            new ("Blog", "\uf781"),
            new ("Bold", "\uf032"),
            new ("Bolt", "\uf0e7"),
            new ("Bomb", "\uf1e2"),
            new ("Bone", "\uf5d7"),
            new ("Bong", "\uf55c"),
            new ("BookDead", "\uf6b7"),
            new ("BookOpen", "\uf518"),
            new ("BookReader", "\uf5da"),
            new ("Book", "\uf02d"),
            new ("Bookmark", "\uf02e"),
            new ("BowlingBall", "\uf436"),
            new ("BoxOpen", "\uf49e"),
            new ("Box", "\uf466"),
            new ("Boxes", "\uf468"),
            new ("Braille", "\uf2a1"),
            new ("Brain", "\uf5dc"),
            new ("BriefcaseMedical", "\uf469"),
            new ("Briefcase", "\uf0b1"),
            new ("BroadcastTower", "\uf519"),
            new ("Broom", "\uf51a"),
            new ("Brush", "\uf55d"),
            new ("Bug", "\uf188"),
            new ("Building", "\uf1ad"),
            new ("Bullhorn", "\uf0a1"),
            new ("Bullseye", "\uf140"),
            new ("Burn", "\uf46a"),
            new ("BusAlt", "\uf55e"),
            new ("Bus", "\uf207"),
            new ("BusinessTime", "\uf64a"),
            new ("Calculator", "\uf1ec"),
            new ("CalendarAlt", "\uf073"),
            new ("CalendarCheck", "\uf274"),
            new ("CalendarDay", "\uf783"),
            new ("CalendarMinus", "\uf272"),
            new ("CalendarPlus", "\uf271"),
            new ("CalendarTimes", "\uf273"),
            new ("CalendarWeek", "\uf784"),
            new ("Calendar", "\uf133"),
            new ("CameraRetro", "\uf083"),
            new ("Camera", "\uf030"),
            new ("Campground", "\uf6bb"),
            new ("CandyCane", "\uf786"),
            new ("Cannabis", "\uf55f"),
            new ("Capsules", "\uf46b"),
            new ("CarAlt", "\uf5de"),
            new ("CarBattery", "\uf5df"),
            new ("CarCrash", "\uf5e1"),
            new ("CarSide", "\uf5e4"),
            new ("Car", "\uf1b9"),
            new ("CaretDown", "\uf0d7"),
            new ("CaretLeft", "\uf0d9"),
            new ("CaretRight", "\uf0da"),
            new ("CaretSquareDown", "\uf150"),
            new ("CaretSquareLeft", "\uf191"),
            new ("CaretSquareRight", "\uf152"),
            new ("CaretSquareUp", "\uf151"),
            new ("CaretUp", "\uf0d8"),
            new ("Carrot", "\uf787"),
            new ("CartArrowDown", "\uf218"),
            new ("CartPlus", "\uf217"),
            new ("CashRegister", "\uf788"),
            new ("Cat", "\uf6be"),
            new ("Certificate", "\uf0a3"),
            new ("Chair", "\uf6c0"),
            new ("ChalkboardTeacher", "\uf51c"),
            new ("Chalkboard", "\uf51b"),
            new ("ChargingStation", "\uf5e7"),
            new ("ChartArea", "\uf1fe"),
            new ("ChartBar", "\uf080"),
            new ("ChartLine", "\uf201"),
            new ("ChartPie", "\uf200"),
            new ("CheckCircle", "\uf058"),
            new ("CheckDouble", "\uf560"),
            new ("CheckSquare", "\uf14a"),
            new ("Check", "\uf00c"),
            new ("ChessBishop", "\uf43a"),
            new ("ChessBoard", "\uf43c"),
            new ("ChessKing", "\uf43f"),
            new ("ChessKnight", "\uf441"),
            new ("ChessPawn", "\uf443"),
            new ("ChessQueen", "\uf445"),
            new ("ChessRook", "\uf447"),
            new ("Chess", "\uf439"),
            new ("ChevronCircleDown", "\uf13a"),
            new ("ChevronCircleLeft", "\uf137"),
            new ("ChevronCircleRight", "\uf138"),
            new ("ChevronCircleUp", "\uf139"),
            new ("ChevronDown", "\uf078"),
            new ("ChevronLeft", "\uf053"),
            new ("ChevronRight", "\uf054"),
            new ("ChevronUp", "\uf077"),
            new ("Child", "\uf1ae"),
            new ("Church", "\uf51d"),
            new ("CircleNotch", "\uf1ce"),
            new ("Circle", "\uf111"),
            new ("City", "\uf64f"),
            new ("ClipboardCheck", "\uf46c"),
            new ("ClipboardList", "\uf46d"),
            new ("Clipboard", "\uf328"),
            new ("Clock", "\uf017"),
            new ("Clone", "\uf24d"),
            new ("ClosedCaptioning", "\uf20a"),
            new ("CloudDownloadAlt", "\uf381"),
            new ("CloudMeatball", "\uf73b"),
            new ("CloudMoonRain", "\uf73c"),
            new ("CloudMoon", "\uf6c3"),
            new ("CloudRain", "\uf73d"),
            new ("CloudShowersHeavy", "\uf740"),
            new ("CloudSunRain", "\uf743"),
            new ("CloudSun", "\uf6c4"),
            new ("CloudUploadAlt", "\uf382"),
            new ("Cloud", "\uf0c2"),
            new ("Cocktail", "\uf561"),
            new ("CodeBranch", "\uf126"),
            new ("Code", "\uf121"),
            new ("Coffee", "\uf0f4"),
            new ("Cog", "\uf013"),
            new ("Cogs", "\uf085"),
            new ("Coins", "\uf51e"),
            new ("Columns", "\uf0db"),
            new ("CommentAlt", "\uf27a"),
            new ("CommentDollar", "\uf651"),
            new ("CommentDots", "\uf4ad"),
            new ("CommentSlash", "\uf4b3"),
            new ("Comment", "\uf075"),
            new ("CommentsDollar", "\uf653"),
            new ("Comments", "\uf086"),
            new ("CompactDisc", "\uf51f"),
            new ("Compass", "\uf14e"),
            new ("CompressArrowsAlt", "\uf78c"),
            new ("Compress", "\uf066"),
            new ("Computer", "\ue4e5"),
            new ("ConciergeBell", "\uf562"),
            new ("CookieBite", "\uf564"),
            new ("Cookie", "\uf563"),
            new ("Copy", "\uf0c5"),
            new ("Copyright", "\uf1f9"),
            new ("Couch", "\uf4b8"),
            new ("CreditCard", "\uf09d"),
            new ("CropAlt", "\uf565"),
            new ("Crop", "\uf125"),
            new ("Cross", "\uf654"),
            new ("Crosshairs", "\uf05b"),
            new ("Crow", "\uf520"),
            new ("Crown", "\uf521"),
            new ("Cube", "\uf1b2"),
            new ("Cubes", "\uf1b3"),
            new ("Cut", "\uf0c4"),
            new ("Database", "\uf1c0"),
            new ("Deaf", "\uf2a4"),
            new ("Democrat", "\uf747"),
            new ("Desktop", "\uf108"),
            new ("Dharmachakra", "\uf655"),
            new ("Diagnoses", "\uf470"),
            new ("DiceD20", "\uf6cf"),
            new ("DiceD6", "\uf6d1"),
            new ("DiceFive", "\uf523"),
            new ("DiceFour", "\uf524"),
            new ("DiceOne", "\uf525"),
            new ("DiceSix", "\uf526"),
            new ("DiceThree", "\uf527"),
            new ("DiceTwo", "\uf528"),
            new ("Dice", "\uf522"),
            new ("DigitalTachograph", "\uf566"),
            new ("Directions", "\uf5eb"),
            new ("Divide", "\uf529"),
            new ("Dizzy", "\uf567"),
            new ("Dna", "\uf471"),
            new ("Dog", "\uf6d3"),
            new ("DollarSign", "\uf155"),
            new ("DollyFlatbed", "\uf474"),
            new ("Dolly", "\uf472"),
            new ("Donate", "\uf4b9"),
            new ("DoorClosed", "\uf52a"),
            new ("DoorOpen", "\uf52b"),
            new ("DotCircle", "\uf192"),
            new ("Dove", "\uf4ba"),
            new ("Download", "\uf019"),
            new ("DraftingCompass", "\uf568"),
            new ("Dragon", "\uf6d5"),
            new ("DrawPolygon", "\uf5ee"),
            new ("DrumSteelpan", "\uf56a"),
            new ("Drum", "\uf569"),
            new ("DrumstickBite", "\uf6d7"),
            new ("Dumbbell", "\uf44b"),
            new ("DumpsterFire", "\uf794"),
            new ("Dumpster", "\uf793"),
            new ("Dungeon", "\uf6d9"),
            new ("Edit", "\uf044"),
            new ("Eject", "\uf052"),
            new ("EllipsisH", "\uf141"),
            new ("EllipsisV", "\uf142"),
            new ("EnvelopeOpenText", "\uf658"),
            new ("EnvelopeOpen", "\uf2b6"),
            new ("EnvelopeSquare", "\uf199"),
            new ("Envelope", "\uf0e0"),
            new ("EqualsSign", "\uf52c"),
            new ("Eraser", "\uf12d"),
            new ("Ethernet", "\uf796"),
            new ("EuroSign", "\uf153"),
            new ("ExchangeAlt", "\uf362"),
            new ("ExclamationCircle", "\uf06a"),
            new ("ExclamationTriangle", "\uf071"),
            new ("Exclamation", "\uf12a"),
            new ("ExpandArrowsAlt", "\uf31e"),
            new ("Expand", "\uf065"),
            new ("ExternalLinkAlt", "\uf35d"),
            new ("ExternalLinkSquareAlt", "\uf360"),
            new ("EyeDropper", "\uf1fb"),
            new ("EyeSlash", "\uf070"),
            new ("Eye", "\uf06e"),
            new ("FastBackward", "\uf049"),
            new ("FastForward", "\uf050"),
            new ("Fax", "\uf1ac"),
            new ("FeatherAlt", "\uf56b"),
            new ("Feather", "\uf52d"),
            new ("Female", "\uf182"),
            new ("FighterJet", "\uf0fb"),
            new ("FileAlt", "\uf15c"),
            new ("FileArchive", "\uf1c6"),
            new ("FileAudio", "\uf1c7"),
            new ("FileCode", "\uf1c9"),
            new ("FileContract", "\uf56c"),
            new ("FileCsv", "\uf6dd"),
            new ("FileDownload", "\uf56d"),
            new ("FileExcel", "\uf1c3"),
            new ("FileExport", "\uf56e"),
            new ("FileImage", "\uf1c5"),
            new ("FileImport", "\uf56f"),
            new ("FileInvoiceDollar", "\uf571"),
            new ("FileInvoice", "\uf570"),
            new ("FileMedicalAlt", "\uf478"),
            new ("FileMedical", "\uf477"),
            new ("FilePdf", "\uf1c1"),
            new ("FilePowerpoint", "\uf1c4"),
            new ("FilePrescription", "\uf572"),
            new ("FileSignature", "\uf573"),
            new ("FileUpload", "\uf574"),
            new ("FileVideo", "\uf1c8"),
            new ("FileWord", "\uf1c2"),
            new ("File", "\uf15b"),
            new ("FillDrip", "\uf576"),
            new ("Fill", "\uf575"),
            new ("Film", "\uf008"),
            new ("Filter", "\uf0b0"),
            new ("Fingerprint", "\uf577"),
            new ("FireAlt", "\uf7e4"),
            new ("FireExtinguisher", "\uf134"),
            new ("Fire", "\uf06d"),
            new ("FirstAid", "\uf479"),
            new ("Fish", "\uf578"),
            new ("FistRaised", "\uf6de"),
            new ("FlagCheckered", "\uf11e"),
            new ("FlagUsa", "\uf74d"),
            new ("Flag", "\uf024"),
            new ("Flask", "\uf0c3"),
            new ("Flushed", "\uf579"),
            new ("FolderMinus", "\uf65d"),
            new ("FolderOpen", "\uf07c"),
            new ("FolderPlus", "\uf65e"),
            new ("Folder", "\uf07b"),
            new ("Font", "\uf031"),
            new ("FootballBall", "\uf44e"),
            new ("Forward", "\uf04e"),
            new ("Frog", "\uf52e"),
            new ("FrownOpen", "\uf57a"),
            new ("Frown", "\uf119"),
            new ("FunnelDollar", "\uf662"),
            new ("Futbol", "\uf1e3"),
            new ("Gamepad", "\uf11b"),
            new ("GasPump", "\uf52f"),
            new ("Gavel", "\uf0e3"),
            new ("Gem", "\uf3a5"),
            new ("Genderless", "\uf22d"),
            new ("Ghost", "\uf6e2"),
            new ("Gift", "\uf06b"),
            new ("Gifts", "\uf79c"),
            new ("GlassCheers", "\uf79f"),
            new ("GlassMartiniAlt", "\uf57b"),
            new ("GlassMartini", "\uf000"),
            new ("GlassWhiskey", "\uf7a0"),
            new ("Glasses", "\uf530"),
            new ("GlobeAfrica", "\uf57c"),
            new ("GlobeAmericas", "\uf57d"),
            new ("GlobeAsia", "\uf57e"),
            new ("GlobeEurope", "\uf7a2"),
            new ("Globe", "\uf0ac"),
            new ("GolfBall", "\uf450"),
            new ("Gopuram", "\uf664"),
            new ("GraduationCap", "\uf19d"),
            new ("GreaterThanEqual", "\uf532"),
            new ("GreaterThan", "\uf531"),
            new ("Grimace", "\uf57f"),
            new ("GrinAlt", "\uf581"),
            new ("GrinBeamSweat", "\uf583"),
            new ("GrinBeam", "\uf582"),
            new ("GrinHearts", "\uf584"),
            new ("GrinSquintTears", "\uf586"),
            new ("GrinSquint", "\uf585"),
            new ("GrinStars", "\uf587"),
            new ("GrinTears", "\uf588"),
            new ("GrinTongueSquint", "\uf58a"),
            new ("GrinTongueWink", "\uf58b"),
            new ("GrinTongue", "\uf589"),
            new ("GrinWink", "\uf58c"),
            new ("Grin", "\uf580"),
            new ("GripHorizontal", "\uf58d"),
            new ("GripLinesVertical", "\uf7a5"),
            new ("GripLines", "\uf7a4"),
            new ("GripVertical", "\uf58e"),
            new ("Guitar", "\uf7a6"),
            new ("HSquare", "\uf0fd"),
            new ("Hammer", "\uf6e3"),
            new ("Hamsa", "\uf665"),
            new ("HandHoldingHeart", "\uf4be"),
            new ("HandHoldingUsd", "\uf4c0"),
            new ("HandHolding", "\uf4bd"),
            new ("HandLizard", "\uf258"),
            new ("HandPaper", "\uf256"),
            new ("HandPeace", "\uf25b"),
            new ("HandPointDown", "\uf0a7"),
            new ("HandPointLeft", "\uf0a5"),
            new ("HandPointRight", "\uf0a4"),
            new ("HandPointUp", "\uf0a6"),
            new ("HandPointer", "\uf25a"),
            new ("HandRock", "\uf255"),
            new ("HandScissors", "\uf257"),
            new ("HandSpock", "\uf259"),
            new ("HandsHelping", "\uf4c4"),
            new ("Hands", "\uf4c2"),
            new ("Handshake", "\uf2b5"),
            new ("Hanukiah", "\uf6e6"),
            new ("Hashtag", "\uf292"),
            new ("HatWizard", "\uf6e8"),
            new ("Haykal", "\uf666"),
            new ("Hdd", "\uf0a0"),
            new ("Heading", "\uf1dc"),
            new ("HeadphonesAlt", "\uf58f"),
            new ("Headphones", "\uf025"),
            new ("Headset", "\uf590"),
            new ("HeartBroken", "\uf7a9"),
            new ("Heart", "\uf004"),
            new ("Heartbeat", "\uf21e"),
            new ("Helicopter", "\uf533"),
            new ("Highlighter", "\uf591"),
            new ("Hiking", "\uf6ec"),
            new ("Hippo", "\uf6ed"),
            new ("History", "\uf1da"),
            new ("HockeyPuck", "\uf453"),
            new ("HollyBerry", "\uf7aa"),
            new ("Home", "\uf015"),
            new ("HorseHead", "\uf7ab"),
            new ("Horse", "\uf6f0"),
            new ("HospitalAlt", "\uf47d"),
            new ("HospitalSymbol", "\uf47e"),
            new ("Hospital", "\uf0f8"),
            new ("HotTub", "\uf593"),
            new ("Hotel", "\uf594"),
            new ("HourglassEnd", "\uf253"),
            new ("HourglassHalf", "\uf252"),
            new ("HourglassStart", "\uf251"),
            new ("Hourglass", "\uf254"),
            new ("HouseDamage", "\uf6f1"),
            new ("Hryvnia", "\uf6f2"),
            new ("ICursor", "\uf246"),
            new ("Icicles", "\uf7ad"),
            new ("IdBadge", "\uf2c1"),
            new ("IdCardAlt", "\uf47f"),
            new ("IdCard", "\uf2c2"),
            new ("Igloo", "\uf7ae"),
            new ("Image", "\uf03e"),
            new ("Images", "\uf302"),
            new ("Inbox", "\uf01c"),
            new ("Indent", "\uf03c"),
            new ("Industry", "\uf275"),
            new ("Infinity", "\uf534"),
            new ("InfoCircle", "\uf05a"),
            new ("Info", "\uf129"),
            new ("Italic", "\uf033"),
            new ("Jedi", "\uf669"),
            new ("Joint", "\uf595"),
            new ("JournalWhills", "\uf66a"),
            new ("Kaaba", "\uf66b"),
            new ("Key", "\uf084"),
            new ("Keyboard", "\uf11c"),
            new ("Khanda", "\uf66d"),
            new ("KissBeam", "\uf597"),
            new ("KissWinkHeart", "\uf598"),
            new ("Kiss", "\uf596"),
            new ("KiwiBird", "\uf535"),
            new ("kitchenSet", "\ue51a"),
            new ("Landmark", "\uf66f"),
            new ("Language", "\uf1ab"),
            new ("LaptopCode", "\uf5fc"),
            new ("Laptop", "\uf109"),
            new ("LaughBeam", "\uf59a"),
            new ("LaughSquint", "\uf59b"),
            new ("LaughWink", "\uf59c"),
            new ("Laugh", "\uf599"),
            new ("LayerGroup", "\uf5fd"),
            new ("Leaf", "\uf06c"),
            new ("Lemon", "\uf094"),
            new ("LessThanEqual", "\uf537"),
            new ("LessThan", "\uf536"),
            new ("LevelDownAlt", "\uf3be"),
            new ("LevelUpAlt", "\uf3bf"),
            new ("LifeRing", "\uf1cd"),
            new ("Lightbulb", "\uf0eb"),
            new ("Link", "\uf0c1"),
            new ("LiraSign", "\uf195"),
            new ("ListAlt", "\uf022"),
            new ("ListOl", "\uf0cb"),
            new ("ListUl", "\uf0ca"),
            new ("List", "\uf03a"),
            new ("LocationArrow", "\uf124"),
            new ("LockOpen", "\uf3c1"),
            new ("Lock", "\uf023"),
            new ("LongArrowAltDown", "\uf309"),
            new ("LongArrowAltLeft", "\uf30a"),
            new ("LongArrowAltRight", "\uf30b"),
            new ("LongArrowAltUp", "\uf30c"),
            new ("LowVision", "\uf2a8"),
            new ("LuggageCart", "\uf59d"),
            new ("Magic", "\uf0d0"),
            new ("Magnet", "\uf076"),
            new ("MailBulk", "\uf674"),
            new ("Male", "\uf183"),
            new ("MapMarkedAlt", "\uf5a0"),
            new ("MapMarked", "\uf59f"),
            new ("MapMarkerAlt", "\uf3c5"),
            new ("MapMarker", "\uf041"),
            new ("MapPin", "\uf276"),
            new ("MapSigns", "\uf277"),
            new ("Map", "\uf279"),
            new ("Marker", "\uf5a1"),
            new ("MarsDouble", "\uf227"),
            new ("MarsStrokeH", "\uf22b"),
            new ("MarsStrokeV", "\uf22a"),
            new ("MarsStroke", "\uf229"),
            new ("Mars", "\uf222"),
            new ("Mask", "\uf6fa"),
            new ("Medal", "\uf5a2"),
            new ("Medkit", "\uf0fa"),
            new ("MehBlank", "\uf5a4"),
            new ("MehRollingEyes", "\uf5a5"),
            new ("Meh", "\uf11a"),
            new ("Memory", "\uf538"),
            new ("Menorah", "\uf676"),
            new ("Mercury", "\uf223"),
            new ("Meteor", "\uf753"),
            new ("Microchip", "\uf2db"),
            new ("MicrophoneAltSlash", "\uf539"),
            new ("MicrophoneAlt", "\uf3c9"),
            new ("MicrophoneSlash", "\uf131"),
            new ("Microphone", "\uf130"),
            new ("Microscope", "\uf610"),
            new ("MinusCircle", "\uf056"),
            new ("MinusSquare", "\uf146"),
            new ("Minus", "\uf068"),
            new ("Mitten", "\uf7b5"),
            new ("MobileAlt", "\uf3cd"),
            new ("Mobile", "\uf10b"),
            new ("MoneyBillAlt", "\uf3d1"),
            new ("MoneyBillWaveAlt", "\uf53b"),
            new ("MoneyBillWave", "\uf53a"),
            new ("MoneyBill", "\uf0d6"),
            new ("MoneyCheckAlt", "\uf53d"),
            new ("MoneyCheck", "\uf53c"),
            new ("Monument", "\uf5a6"),
            new ("Moon", "\uf186"),
            new ("MortarPestle", "\uf5a7"),
            new ("Mosque", "\uf678"),
            new ("Motorcycle", "\uf21c"),
            new ("Mountain", "\uf6fc"),
            new ("MousePointer", "\uf245"),
            new ("MugHot", "\uf7b6"),
            new ("Music", "\uf001"),
            new ("NetworkWired", "\uf6ff"),
            new ("Neuter", "\uf22c"),
            new ("Newspaper", "\uf1ea"),
            new ("NotEqual", "\uf53e"),
            new ("NotesMedical", "\uf481"),
            new ("ObjectGroup", "\uf247"),
            new ("ObjectUngroup", "\uf248"),
            new ("OilCan", "\uf613"),
            new ("Om", "\uf679"),
            new ("Otter", "\uf700"),
            new ("Outdent", "\uf03b"),
            new ("PaintBrush", "\uf1fc"),
            new ("PaintRoller", "\uf5aa"),
            new ("Palette", "\uf53f"),
            new ("Pallet", "\uf482"),
            new ("PaperPlane", "\uf1d8"),
            new ("Paperclip", "\uf0c6"),
            new ("ParachuteBox", "\uf4cd"),
            new ("Paragraph", "\uf1dd"),
            new ("Parking", "\uf540"),
            new ("Passport", "\uf5ab"),
            new ("Pastafarianism", "\uf67b"),
            new ("Paste", "\uf0ea"),
            new ("PauseCircle", "\uf28b"),
            new ("Pause", "\uf04c"),
            new ("Paw", "\uf1b0"),
            new ("Peace", "\uf67c"),
            new ("PenAlt", "\uf305"),
            new ("PenFancy", "\uf5ac"),
            new ("PenNib", "\uf5ad"),
            new ("PenSquare", "\uf14b"),
            new ("Pen", "\uf304"),
            new ("PencilAlt", "\uf303"),
            new ("PencilRuler", "\uf5ae"),
            new ("PeopleCarry", "\uf4ce"),
            new ("Percent", "\uf295"),
            new ("Percentage", "\uf541"),
            new ("PersonBooth", "\uf756"),
            new ("PhoneSlash", "\uf3dd"),
            new ("PhoneSquare", "\uf098"),
            new ("PhoneVolume", "\uf2a0"),
            new ("Phone", "\uf095"),
            new ("PiggyBank", "\uf4d3"),
            new ("Pills", "\uf484"),
            new ("PlaceOfWorship", "\uf67f"),
            new ("PlaneArrival", "\uf5af"),
            new ("PlaneDeparture", "\uf5b0"),
            new ("Plane", "\uf072"),
            new ("PlayCircle", "\uf144"),
            new ("Play", "\uf04b"),
            new ("Plug", "\uf1e6"),
            new ("PlusCircle", "\uf055"),
            new ("PlusSquare", "\uf0fe"),
            new ("Plus", "\uf067"),
            new ("Podcast", "\uf2ce"),
            new ("PollH", "\uf682"),
            new ("Poll", "\uf681"),
            new ("PooStorm", "\uf75a"),
            new ("Poo", "\uf2fe"),
            new ("Poop", "\uf619"),
            new ("Portrait", "\uf3e0"),
            new ("PoundSign", "\uf154"),
            new ("PowerOff", "\uf011"),
            new ("Pray", "\uf683"),
            new ("PrayingHands", "\uf684"),
            new ("PrescriptionBottleAlt", "\uf486"),
            new ("PrescriptionBottle", "\uf485"),
            new ("Prescription", "\uf5b1"),
            new ("Print", "\uf02f"),
            new ("Procedures", "\uf487"),
            new ("ProjectDiagram", "\uf542"),
            new ("PuzzlePiece", "\uf12e"),
            new ("Qrcode", "\uf029"),
            new ("QuestionCircle", "\uf059"),
            new ("Question", "\uf128"),
            new ("Quidditch", "\uf458"),
            new ("QuoteLeft", "\uf10d"),
            new ("QuoteRight", "\uf10e"),
            new ("Quran", "\uf687"),
            new ("RadiationAlt", "\uf7ba"),
            new ("Radiation", "\uf7b9"),
            new ("Rainbow", "\uf75b"),
            new ("Random", "\uf074"),
            new ("Receipt", "\uf543"),
            new ("Recycle", "\uf1b8"),
            new ("RedoAlt", "\uf2f9"),
            new ("Redo", "\uf01e"),
            new ("Registered", "\uf25d"),
            new ("ReplyAll", "\uf122"),
            new ("Reply", "\uf3e5"),
            new ("Republican", "\uf75e"),
            new ("Restroom", "\uf7bd"),
            new ("Retweet", "\uf079"),
            new ("Ribbon", "\uf4d6"),
            new ("Ring", "\uf70b"),
            new ("Road", "\uf018"),
            new ("Robot", "\uf544"),
            new ("Rocket", "\uf135"),
            new ("Route", "\uf4d7"),
            new ("RssSquare", "\uf143"),
            new ("Rss", "\uf09e"),
            new ("RubleSign", "\uf158"),
            new ("RulerCombined", "\uf546"),
            new ("RulerHorizontal", "\uf547"),
            new ("RulerVertical", "\uf548"),
            new ("Ruler", "\uf545"),
            new ("Running", "\uf70c"),
            new ("RupeeSign", "\uf156"),
            new ("SadCry", "\uf5b3"),
            new ("SadTear", "\uf5b4"),
            new ("SatelliteDish", "\uf7c0"),
            new ("Satellite", "\uf7bf"),
            new ("Save", "\uf0c7"),
            new ("School", "\uf549"),
            new ("Screwdriver", "\uf54a"),
            new ("Scroll", "\uf70e"),
            new ("SdCard", "\uf7c2"),
            new ("SearchDollar", "\uf688"),
            new ("SearchLocation", "\uf689"),
            new ("SearchMinus", "\uf010"),
            new ("SearchPlus", "\uf00e"),
            new ("Search", "\uf002"),
            new ("Seedling", "\uf4d8"),
            new ("Server", "\uf233"),
            new ("Shapes", "\uf61f"),
            new ("ShareAltSquare", "\uf1e1"),
            new ("ShareAlt", "\uf1e0"),
            new ("ShareSquare", "\uf14d"),
            new ("Share", "\uf064"),
            new ("ShekelSign", "\uf20b"),
            new ("ShieldAlt", "\uf3ed"),
            new ("Ship", "\uf21a"),
            new ("ShippingFast", "\uf48b"),
            new ("ShoePrints", "\uf54b"),
            new ("ShoppingBag", "\uf290"),
            new ("ShoppingBasket", "\uf291"),
            new ("ShoppingCart", "\uf07a"),
            new ("Shower", "\uf2cc"),
            new ("ShuttleVan", "\uf5b6"),
            new ("SignInAlt", "\uf2f6"),
            new ("SignLanguage", "\uf2a7"),
            new ("SignOutAlt", "\uf2f5"),
            new ("Sign", "\uf4d9"),
            new ("Signal", "\uf012"),
            new ("Signature", "\uf5b7"),
            new ("SimCard", "\uf7c4"),
            new ("Sitemap", "\uf0e8"),
            new ("Skating", "\uf7c5"),
            new ("SkiingNordic", "\uf7ca"),
            new ("Skiing", "\uf7c9"),
            new ("SkullCrossbones", "\uf714"),
            new ("Skull", "\uf54c"),
            new ("Slash", "\uf715"),
            new ("Sleigh", "\uf7cc"),
            new ("SlidersH", "\uf1de"),
            new ("SmileBeam", "\uf5b8"),
            new ("SmileWink", "\uf4da"),
            new ("Smile", "\uf118"),
            new ("Smog", "\uf75f"),
            new ("SmokingBan", "\uf54d"),
            new ("Smoking", "\uf48d"),
            new ("Sms", "\uf7cd"),
            new ("Snowboarding", "\uf7ce"),
            new ("Snowflake", "\uf2dc"),
            new ("Snowman", "\uf7d0"),
            new ("Snowplow", "\uf7d2"),
            new ("Socks", "\uf696"),
            new ("SolarPanel", "\uf5ba"),
            new ("SortAlphaDown", "\uf15d"),
            new ("SortAlphaUp", "\uf15e"),
            new ("SortAmountDown", "\uf160"),
            new ("SortAmountUp", "\uf161"),
            new ("SortDown", "\uf0dd"),
            new ("SortNumericDown", "\uf162"),
            new ("SortNumericUp", "\uf163"),
            new ("SortUp", "\uf0de"),
            new ("Sort", "\uf0dc"),
            new ("Spa", "\uf5bb"),
            new ("SpaceShuttle", "\uf197"),
            new ("Spider", "\uf717"),
            new ("Spinner", "\uf110"),
            new ("Splotch", "\uf5bc"),
            new ("SprayCan", "\uf5bd"),
            new ("SquareFull", "\uf45c"),
            new ("SquareRootAlt", "\uf698"),
            new ("Square", "\uf04d"),
            new ("Stamp", "\uf5bf"),
            new ("StarAndCrescent", "\uf699"),
            new ("StarHalfAlt", "\uf5c0"),
            new ("StarHalf", "\uf089"),
            new ("StarOfDavid", "\uf69a"),
            new ("StarOfLife", "\uf621"),
            new ("Star", "\uf005"),
            new ("StepBackward", "\uf048"),
            new ("StepForward", "\uf051"),
            new ("Stethoscope", "\uf0f1"),
            new ("StickyNote", "\uf249"),
            new ("StopCircle", "\uf28d"),
            new ("Stopwatch", "\uf2f2"),
            new ("StoreAlt", "\uf54f"),
            new ("Store", "\uf54e"),
            new ("Stream", "\uf550"),
            new ("StreetView", "\uf21d"),
            new ("Strikethrough", "\uf0cc"),
            new ("Stroopwafel", "\uf551"),
            new ("Subscript", "\uf12c"),
            new ("Subway", "\uf239"),
            new ("SuitcaseRolling", "\uf5c1"),
            new ("Suitcase", "\uf0f2"),
            new ("Sun", "\uf185"),
            new ("Superscript", "\uf12b"),
            new ("Surprise", "\uf5c2"),
            new ("Swatchbook", "\uf5c3"),
            new ("Swimmer", "\uf5c4"),
            new ("SwimmingPool", "\uf5c5"),
            new ("Synagogue", "\uf69b"),
            new ("SyncAlt", "\uf2f1"),
            new ("Sync", "\uf021"),
            new ("Syringe", "\uf48e"),
            new ("TableTennis", "\uf45d"),
            new ("Table", "\uf0ce"),
            new ("TabletAlt", "\uf3fa"),
            new ("Tablet", "\uf10a"),
            new ("Tablets", "\uf490"),
            new ("TachometerAlt", "\uf3fd"),
            new ("Tag", "\uf02b"),
            new ("Tags", "\uf02c"),
            new ("Tape", "\uf4db"),
            new ("Tasks", "\uf0ae"),
            new ("Taxi", "\uf1ba"),
            new ("TeethOpen", "\uf62f"),
            new ("Teeth", "\uf62e"),
            new ("TemperatureHigh", "\uf769"),
            new ("TemperatureLow", "\uf76b"),
            new ("Tenge", "\uf7d7"),
            new ("Terminal", "\uf120"),
            new ("TextHeight", "\uf034"),
            new ("TextWidth", "\uf035"),
            new ("ThLarge", "\uf009"),
            new ("ThList", "\uf00b"),
            new ("Th", "\uf00a"),
            new ("TheaterMasks", "\uf630"),
            new ("ThermometerEmpty", "\uf2cb"),
            new ("ThermometerFull", "\uf2c7"),
            new ("ThermometerHalf", "\uf2c9"),
            new ("ThermometerQuarter", "\uf2ca"),
            new ("ThermometerThreeQuarters", "\uf2c8"),
            new ("Thermometer", "\uf491"),
            new ("ThumbsDown", "\uf165"),
            new ("ThumbsUp", "\uf164"),
            new ("Thumbtack", "\uf08d"),
            new ("TicketAlt", "\uf3ff"),
            new ("TimesCircle", "\uf057"),
            new ("Times", "\uf00d"),
            new ("TintSlash", "\uf5c7"),
            new ("Tint", "\uf043"),
            new ("Tired", "\uf5c8"),
            new ("ToggleOff", "\uf204"),
            new ("ToggleOn", "\uf205"),
            new ("ToiletPaper", "\uf71e"),
            new ("Toilet", "\uf7d8"),
            new ("Toolbox", "\uf552"),
            new ("Tools", "\uf7d9"),
            new ("Tooth", "\uf5c9"),
            new ("Torah", "\uf6a0"),
            new ("ToriiGate", "\uf6a1"),
            new ("Tractor", "\uf722"),
            new ("Trademark", "\uf25c"),
            new ("TrafficLight", "\uf637"),
            new ("Train", "\uf238"),
            new ("Tram", "\uf7da"),
            new ("TransgenderAlt", "\uf225"),
            new ("Transgender", "\uf224"),
            new ("TrashAlt", "\uf2ed"),
            new ("Trash", "\uf1f8"),
            new ("Tree", "\uf1bb"),
            new ("Trophy", "\uf091"),
            new ("TruckLoading", "\uf4de"),
            new ("TruckMonster", "\uf63b"),
            new ("TruckMoving", "\uf4df"),
            new ("TruckPickup", "\uf63c"),
            new ("Truck", "\uf0d1"),
            new ("Tshirt", "\uf553"),
            new ("Tty", "\uf1e4"),
            new ("Tv", "\uf26c"),
            new ("UmbrellaBeach", "\uf5ca"),
            new ("Umbrella", "\uf0e9"),
            new ("Underline", "\uf0cd"),
            new ("UndoAlt", "\uf2ea"),
            new ("Undo", "\uf0e2"),
            new ("UniversalAccess", "\uf29a"),
            new ("University", "\uf19c"),
            new ("Unlink", "\uf127"),
            new ("UnlockAlt", "\uf13e"),
            new ("Unlock", "\uf09c"),
            new ("Upload", "\uf093"),
            new ("UserAltSlash", "\uf4fa"),
            new ("UserAlt", "\uf406"),
            new ("UserAstronaut", "\uf4fb"),
            new ("UserCheck", "\uf4fc"),
            new ("UserCircle", "\uf2bd"),
            new ("UserClock", "\uf4fd"),
            new ("UserCog", "\uf4fe"),
            new ("UserEdit", "\uf4ff"),
            new ("UserFriends", "\uf500"),
            new ("UserGraduate", "\uf501"),
            new ("UserInjured", "\uf728"),
            new ("UserLock", "\uf502"),
            new ("UserMd", "\uf0f0"),
            new ("UserMinus", "\uf503"),
            new ("UserNinja", "\uf504"),
            new ("UserPlus", "\uf234"),
            new ("UserSecret", "\uf21b"),
            new ("UserShield", "\uf505"),
            new ("UserSlash", "\uf506"),
            new ("UserTag", "\uf507"),
            new ("UserTie", "\uf508"),
            new ("UserTimes", "\uf235"),
            new ("User", "\uf007"),
            new ("UsersCog", "\uf509"),
            new ("Users", "\uf0c0"),
            new ("UtensilSpoon", "\uf2e5"),
            new ("Utensils", "\uf2e7"),
            new ("VectorSquare", "\uf5cb"),
            new ("VenusDouble", "\uf226"),
            new ("VenusMars", "\uf228"),
            new ("Venus", "\uf221"),
            new ("Vial", "\uf492"),
            new ("Vials", "\uf493"),
            new ("VideoSlash", "\uf4e2"),
            new ("Video", "\uf03d"),
            new ("Vihara", "\uf6a7"),
            new ("VolleyballBall", "\uf45f"),
            new ("VolumeDown", "\uf027"),
            new ("VolumeMute", "\uf6a9"),
            new ("VolumeOff", "\uf026"),
            new ("VolumeUp", "\uf028"),
            new ("VoteYea", "\uf772"),
            new ("VrCardboard", "\uf729"),
            new ("Walking", "\uf554"),
            new ("Wallet", "\uf555"),
            new ("Warehouse", "\uf494"),
            new ("Water", "\uf773"),
            new ("WeightHanging", "\uf5cd"),
            new ("Weight", "\uf496"),
            new ("Wheelchair", "\uf193"),
            new ("Wifi", "\uf1eb"),
            new ("Wind", "\uf72e"),
            new ("WindowClose", "\uf410"),
            new ("WindowMaximize", "\uf2d0"),
            new ("WindowMinimize", "\uf2d1"),
            new ("WindowRestore", "\uf2d2"),
            new ("WineBottle", "\uf72f"),
            new ("WineGlassAlt", "\uf5ce"),
            new ("WineGlass", "\uf4e3"),
            new ("WonSign", "\uf159"),
            new ("Wrench", "\uf0ad"),
            new ("XRay", "\uf497"),
            new ("YenSign", "\uf157"),
            new ("YinYang", "\uf6ad"),
        };
    }
}
