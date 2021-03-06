var execBtn = document.getElementById("execute");
var outputElm = document.getElementById('output');
var errorElm = document.getElementById('error');
var commandsElm = document.getElementById('commands');
var dbFileElm = document.getElementById('dbfile');
var savedbElm = document.getElementById('savedb');
var testCaseElm = document.getElementById('testcase_output')
var hintBtn = document.getElementById('hint')
var hintAreaElm = document.getElementById('hint_area')

var nohintClass = document.getElementsByClassName('nohint')

var worker = new Worker("../../dist/worker.sql-wasm-debug.js");
worker.onerror = error;

// Open a database
worker.postMessage({ action: 'open' });

// Connect to the HTML element we 'print' to
function print(text) {
	outputElm.innerHTML = text.replace(/\n/g, '<br>');
}

function loadBookDB() {
	commands = "PRAGMA foreign_keys=off; \
 	DROP TABLE IF EXISTS TradeTransactions; \
	DROP TABLE IF EXISTS GuildTreasury; \
	DROP TABLE IF EXISTS Items; \
	DROP TABLE IF EXISTS Guilds; \
	DROP TABLE IF EXISTS Players; \
	CREATE TABLE Players( 		\
      		playerID integer primary key,  			\
      		playerName varchar(255),                 	\
      		playerLevel integer,				\
      		guildID integer,				\
      		coins integer,				\
		FOREIGN KEY (guildID) REFERENCES Guilds(guildID)	\
	);						\
	CREATE TABLE Guilds( 				\
      		guildID integer primary key,  			\
      		guildName    varchar(255),                 	\
      		guildLevel integer,				\
		dateCreated date,			\
		leader integer,				\
		FOREIGN KEY (leader) REFERENCES Players(playerID)	\
	);						\
	CREATE TABLE Items( 				\
      		itemID integer primary key,  			\
      		itemName    varchar(255),                 	\
      		minLevel integer,				\
		type VARCHAR(255),			\
		handedness int				\
	);						\
	CREATE TABLE GuildTreasury ( 				\
      		guildID integer,  			\
      		itemID integer,                 	\
		quantity integer,			\
		FOREIGN KEY (guildID) REFERENCES Guilds(guildID),	\
		FOREIGN KEY (itemID) REFERENCES Items(itemID)	\
	);						\
	CREATE TABLE TradeTransactions ( 				\
		transactionID integer primary key,				\
      		sendingPlayerID integer,  			\
      		receivingPlayerID integer,                 	\
		transactionTime datetime,			\
		itemID integer,					\
		FOREIGN KEY (sendingPlayerID) REFERENCES Players(playerID),	\
		FOREIGN KEY (receivingPlayerID) REFERENCES Players(playerID),	\
		FOREIGN KEY (itemID) REFERENCES Items(itemID)	\
	);						\
  INSERT INTO Players VALUES (1, 'Elyse', 21, 10, 1234); \
  INSERT INTO Players VALUES (2, 'Alyma', 18, 20, 2133); \
  INSERT INTO Players VALUES (3, 'Kennis', 8, 10, 453); \
  INSERT INTO Players VALUES (4, 'Blothie', 2, 20, 120); \
  INSERT INTO Players VALUES (5, 'Radix', 8, 20, 529); \
  INSERT INTO Players VALUES (6, 'Apl', 1, NULL, 1); \
  INSERT INTO Players VALUES (7, 'Babbage', 2, 20, 111); \
  INSERT INTO Players VALUES (8, 'Cait', 11, NULL, 742); \
  INSERT INTO Players VALUES (9, 'Mintee', 19, 30, 889); \
  INSERT INTO Players VALUES (10, 'Wraithse', 12, 30, 951); \
  INSERT INTO Players VALUES (11, 'Plucki', 11, 40, 112); \
  INSERT INTO Players VALUES (12, 'Sava', 15, NULL, 1021); \
  INSERT INTO Players VALUES (13, 'Vera', 9, 50, 831); \
  INSERT INTO Players VALUES (14, 'Aventop', 3, 60, 1); \
  INSERT INTO Players VALUES (15, 'Kylomer', 4, 60, 104); \
  INSERT INTO Players VALUES (16, 'Rydomin', 6, 60, 114); \
  INSERT INTO Players VALUES (17, 'Sulin', 5, 60, 117); \
  INSERT INTO Players VALUES (18, 'Xylo', 1, 70, 2); \
  INSERT INTO Players VALUES (19, 'Penni', 4, 20, 201); \
  INSERT INTO Guilds VALUES (10, 'Grey Warriors', 20, '2019-05-03', 1); \
  INSERT INTO Guilds VALUES (20, 'Shocking Power', 18, '2019-05-04', 2); \
  INSERT INTO Guilds VALUES (30, 'Shimmering Light', 1, '2019-04-19', 9); \
  INSERT INTO Guilds VALUES (40, 'Gray Wolf Clan', 2, '2019-04-20', 11); \
  INSERT INTO Guilds VALUES (50, 'Winds of Grey', 4, '2019-04-25', 13); \
  INSERT INTO Guilds VALUES (60, 'Grey Mountaineers', 7, '2019-06-01', 17); \
  INSERT INTO Guilds VALUES (70, 'Bitter Power Pals', 3, '2019-04-25', NULL); \
  INSERT INTO Guilds VALUES (80, 'Vengeful Warriors', 4, '2019-05-11', NULL); \
  INSERT INTO Items VALUES (100, 'Iron Sword', 5, 'sword', 1); \
  INSERT INTO Items VALUES (200, 'Steel Battleaxe', 8, 'axe', 2); \
  INSERT INTO Items VALUES (300, 'Steel Bow', 7, 'bow', 2); \
  INSERT INTO Items VALUES (400, 'Bronze Axe', 10, 'axe', 1); \
  INSERT INTO Items VALUES (500, 'Bronze Bow', 10, 'bow', 2); \
  INSERT INTO Items VALUES (600, 'Silver Dagger', 15, 'dagger', 1); \
  INSERT INTO Items VALUES (700, 'Steel Dagger', 1, 'dagger', 1); \
  INSERT INTO Items VALUES (800, 'Iron Staff', 2, 'staff', 2); \
  INSERT INTO Items VALUES (900, 'Dwarven Axe', 18, 'axe', 1); \
  INSERT INTO GuildTreasury VALUES (10, 300, 3); \
  INSERT INTO GuildTreasury VALUES (10, 900, 1); \
  INSERT INTO GuildTreasury VALUES (20, 700, 2); \
  INSERT INTO GuildTreasury VALUES (20, 200, 11); \
  INSERT INTO GuildTreasury VALUES (60, 100, 6); \
  INSERT INTO GuildTreasury VALUES (10, 100, 12); \
  INSERT INTO GuildTreasury VALUES (50, 400, 1); \
  INSERT INTO GuildTreasury VALUES (30, 200, 14); \
  INSERT INTO GuildTreasury VALUES (20, 600, 4); \
  INSERT INTO GuildTreasury VALUES (20, 800, 1); \
  INSERT INTO GuildTreasury VALUES (20, 900, 1); \
  INSERT INTO GuildTreasury VALUES (40, 300, 4); \
  INSERT INTO GuildTreasury VALUES (30, 300, 10); \
  INSERT INTO GuildTreasury VALUES (30, 400, 7); \
  INSERT INTO GuildTreasury VALUES (20, 400, 8); \
  INSERT INTO GuildTreasury VALUES (40, 200, 6); \
  INSERT INTO GuildTreasury VALUES (40, 600, 4); \
  INSERT INTO GuildTreasury VALUES (20, 100, 14); \
  INSERT INTO GuildTreasury VALUES (40, 800, 2); \
  INSERT INTO GuildTreasury VALUES (50, 200, 2); \
  INSERT INTO GuildTreasury VALUES (10, 700, 1); \
  INSERT INTO GuildTreasury VALUES (50, 900, 3); \
  INSERT INTO GuildTreasury VALUES (30, 100, 16); \
  INSERT INTO GuildTreasury VALUES (60, 400, 3); \
  INSERT INTO TradeTransactions VALUES (1, 2, 4, '2019-04-02 10:01:03', 100); \
  INSERT INTO TradeTransactions VALUES (2, 3, 4, '2019-04-04 10:01:04', 300); \
  INSERT INTO TradeTransactions VALUES (3, 4, 1, '2019-04-04 11:07:15', 100); \
  INSERT INTO TradeTransactions VALUES (4, 10, 6, '2019-04-06 10:01:28', 600); \
  INSERT INTO TradeTransactions VALUES (5, 10, 3, '2019-04-09 10:01:52', 800); \
  INSERT INTO TradeTransactions VALUES (6, 7, 2, '2019-04-17 10:01:21', 900); \
  INSERT INTO TradeTransactions VALUES (7, 9, 11, '2019-04-22 10:01:12', 900); \
  INSERT INTO TradeTransactions VALUES (8, 1, 12, '2019-04-28 10:01:46', 100); \
  INSERT INTO TradeTransactions VALUES (9, 2, 13, '2019-05-03 14:01:03', 200); \
  INSERT INTO TradeTransactions VALUES (10, 2, 13, '2019-05-03 14:01:59', 300); \
  INSERT INTO TradeTransactions VALUES (11, 16, 1, '2019-05-04 05:01:03', 400); \
  INSERT INTO TradeTransactions VALUES (12, 17, 4, '2019-05-12 22:01:53', 300); \
  INSERT INTO TradeTransactions VALUES (13, 6, 3, '2019-05-19 16:42:16', 300); \
  INSERT INTO TradeTransactions VALUES (14, 9, 11, '2019-05-29 10:01:03', 600); \
  INSERT INTO TradeTransactions VALUES (15, 12, 2, '2019-05-30 12:59:21', 600); \
  INSERT INTO TradeTransactions VALUES (15, 12, 2, '2019-05-30 13:01:02', 900); \
  INSERT INTO TradeTransactions VALUES (16, 2, 14, '2019-05-30 13:02:12', 200); \
  INSERT INTO TradeTransactions VALUES (17, 3, 2, '2019-05-30 13:05:38', 700); \
  INSERT INTO TradeTransactions VALUES (18, 7, 4, '2019-05-30 13:07:03', 300); \
  INSERT INTO TradeTransactions VALUES (19, 2, 4, '2019-05-30 13:52:11', 200); \
  INSERT INTO TradeTransactions VALUES (20, 9, 19, '2019-05-30 13:52:44', 200); \
  INSERT INTO TradeTransactions VALUES (21, 9, 19, '2019-05-30 14:00:51', 100); \
  INSERT INTO TradeTransactions VALUES (24, 4, 6, '2019-06-09 08:17:49', 800); \
  INSERT INTO TradeTransactions VALUES (25, 4, 7, '2019-06-10 10:32:00', 700); \
  INSERT INTO TradeTransactions VALUES (26, 6, 4, '2019-06-16 14:31:41', 200); \
  INSERT INTO TradeTransactions VALUES (22, 10, 6, '2019-06-21 10:01:01', 100); \
  INSERT INTO TradeTransactions VALUES (23, 10, 7, '2019-06-21 12:01:04', 300); \
  INSERT INTO TradeTransactions VALUES (27, 5, 12, '2019-06-21 14:01:29', 100); \
  INSERT INTO TradeTransactions VALUES (28, 1, 17, '2019-06-21 14:01:33', 900); \
  INSERT INTO TradeTransactions VALUES (29, 5, 1, '2019-07-12 10:01:30', 700); \
  INSERT INTO TradeTransactions VALUES (30, 7, 3, '2019-07-13 10:01:33', 700); \
  PRAGMA foreign_keys=on; \
";
	worker.postMessage({ action: 'exec', sql: commands });
}
function error(e) {
	console.log(e);
	errorElm.style.height = '2em';
	errorElm.textContent = e.message;
}

function noerror() {
	errorElm.style.height = '2em';
	errorElm.textContent = " ";
}

// Run a command in the database
function execute(commands) {
	testCaseElm.innerHTML = ""

	var username = document.getElementsByClassName("fs-block");
	if (username) {
		console.log(username)
		console.log("USERNAME: " + username)
	}
	noerror();
	tic();
	worker.onmessage = function (event) {
		if (event.data.error)
		{
			event.message = event.data.error;
			error(event);
		}

		var results = event.data.results;
		toc("Executing SQL");

		tic();
		outputElm.innerHTML = "";
		for (var i = 0; i < results.length; i++) {
			runAsserts(results[i])
			outputElm.appendChild(tableCreate(results[i].columns, results[i].values));
		}
		toc("Displaying results");
	}
	
	worker.postMessage({ action: 'exec', sql: commands });
	outputElm.textContent = "Fetching results...";
}

var runAsserts = function(results) {
	const params = new URLSearchParams(window.location.search)
	if (params.has('q')) {
		var question = params.get('q')

		// What I'd really like to do is load a file with the assert list
		let url = question + '.txt';
		let request = new XMLHttpRequest();
		request.open('GET', url);
		request.responseType = 'text';
		request.onload = function() {
			checkAsserts(results, request.response)
		  };
		request.send()
	}
}

var checkAsserts = function(result_table, test_list) {
	var tests = test_list.split(/\n/);
	this.code_word = tests.shift()
	this.passed = 0;
	this.failed = 0;
	// Tests should be of the form
	// assert row,col oper value for example
	// assert 4,4 == 3
	var result = "";
	for (let test of tests) {
		let wlist = test.split(/\s+/);
		console.log(wlist)
		let assertType = wlist.shift();
		if (assertType == 'V') {
			let loc = wlist.shift();
			let oper = wlist.shift();
			let expected = wlist.join(" ");
			let [row, col] = loc.split(",");
			result += this.testValueAssert(
				row,
				col,
				oper,
				expected,
				result_table
			);
		} else if (assertType == "C") {
			let loc = wlist.shift();
			let oper = wlist.shift();
			let expected = wlist.join(" ").trim();
			result += this.testColumnAssert(
				loc,
				oper,
				expected,
				result_table
			);

		} else if (assertType == "L") {
			let length_type = wlist.shift();
			let expected = wlist.shift();
			result += this.testLengthAssert(
				length_type,
				expected,
				result_table
			);
		}
		result += "\n";
	}
	let pct = (100 * this.passed) / (this.passed + this.failed);
	pct = pct.toLocaleString(undefined, { maximumFractionDigits: 2 });
	result += `You passed ${this.passed} out of ${
		this.passed + this.failed
	} tests for ${pct}%`;
	if (this.failed == 0) {
		result += "\nCodeword is " + code_word
	}
	testCaseElm.innerHTML += result
}

var addPassMessage = function(message) {
	output = "<div class='pass'>" + message + "</div>"
	testCaseElm.innerHTML += output
}

var addFailMessage = function(message) {
	output = "<div class='fail'>" + message + "</div>"
	testCaseElm.innerHTML += output
}

var testLengthAssert = function(length_type, expected, result_table){ 
	if (length_type == "R") {
		var actual = result_table.values.length
	} else if (length_type == "C") {
		var actual = result_table.columns.length
	}
	let output = "";
	let res = expected == actual;
	if (res) {
		if (length_type == "R") {
			addPassMessage(`Pass: ${actual} == ${expected} for number of rows</div>`);
		} else if (length_type == "C") {
			addPassMessage(`Pass: ${actual} == ${expected} for number of columns`);
		}
		this.passed++;
	} else {
		if (length_type == "R") {
			addFailMessage(`Fail: ${actual} == ${expected} for number of rows`);
		} else if (length_type == "C") {
			addFailMessage(`Fail: ${actual} == ${expected} for number of columns`);
		}
		this.failed++;
	}
	return output;
}

const operators = {
	"==": function (operand1, operand2) {
		return operand1 == operand2;
	},
	"!=": function (operand1, operand2) {
		return operand1 != operand2;
	},
	">": function (operand1, operand2) {
		return operand1 > operand2;
	},
	"<": function (operand1, operand2) {
		return operand1 > operand2;
	},
};


var testColumnAssert = function(loc, oper, expected, result_table){ 
	let actual = result_table.columns[loc];
	let output = "";
	console.log(actual == expected)
	console.log("'" + actual + "'")
	console.log("'" + expected + "'")

	let res = operators[oper](actual, expected);
	if (res) {
		addPassMessage(`Pass: ${actual} ${oper} ${expected} for column name in column ${loc}`);
		this.passed++;
	} else {
		addFailMessage(`Fail: ${actual} ${oper} ${expected} for column name in column ${loc}`);
		this.failed++;
	}
	return output;

}

var testValueAssert = function (row, col, oper, expected, result_table){
	let actual = result_table.values[row][col];
	let output = "";

	let res = operators[oper](actual, expected);
	if (res) {
		addPassMessage(`Pass: ${actual} ${oper} ${expected} in row ${row} column ${result_table.columns[col]}`);
		this.passed++;
	} else {
		addFailMessage(`Fail: ${actual} ${oper} ${expected} in row ${row} column ${result_table.columns[col]}`);
		this.failed++;
	}
	return output;
}


// Create an HTML table
var tableCreate = function () {
	function valconcat(vals, tagName) {
		if (vals.length === 0) return '';
		// Replace empty null values with NULL text
		for (var i = 0; i < vals.length; i++) {
			if (vals[i] === null) {
				vals[i] = "NULL";
			}
		}

		var open = '<' + tagName + '>', close = '</' + tagName + '>';
		return open + vals.join(close + open) + close;
	}
	return function (columns, values) {
		var tbl = document.createElement('table');
		var html = '<thead>' + valconcat(columns, 'th') + '</thead>';
		var rows = values.map(function (v) { return valconcat(v, 'td'); });
		html += '<tbody>' + valconcat(rows, 'tr') + '</tbody>';
		tbl.innerHTML = html;
		return tbl;
	}
}();

// Execute the commands when the button is clicked
function execEditorContents() {
	// Start the worker in which sql.js will run
	noerror()
	execute(editor.getValue() + ';');
}
execBtn.addEventListener("click", execEditorContents, true);

// Execute the commands when the button is clicked
function hintClicked() {
	console.log("hint clicked");
	//Start the Parson's Problem interface
		  var initial = "SELECT $$toggle::*::playerId::playerName$$\n" +
					"FROM $$toggle::Players::Guilds::Items$$\n" +
					"WHERE $$toggle::playerId::playerName$$ = " +
					"$$toggle::\"Alyma\"::\"Elyse\"$$\n";
	;
	  $(document).ready(function(){
		var parson = new ParsonsWidget({
		  'sortableId': 'sortable',
		  'trashId': 'sortableTrash',
		  'vartests': [],
		  lang: "en",
		  'grader': ParsonsWidget._graders.LanguageTranslationGrader,
		  'executable_code': initial,
		  'programmingLang': "pseudo",
		  can_indent: false
		});
		parson.init(initial);
		parson.shuffleLines();
		$("#newInstanceLink").click(function(event){
		  event.preventDefault();
		  parson.shuffleLines();
		});
		$("#feedbackLink").click(function(event){
			executable_code = parson.grader._replaceCodelines()
			console.log(executable_code);
			execute(executable_code + ';');
		});
	  });

	
	
	//Remove the SQL interpreter
	
	
	var hintAreaElm = document.getElementById("hint_area");
	if (hintAreaElm.style.display === "none") {
		hintAreaElm.style.display = "block";
	} else {
		hintAreaElm.style.display = "none";
	}
	[].forEach.call(document.querySelectorAll('.nohint'), function (el) {
		el.style.display = 'none';
	});
	[].forEach.call(document.querySelectorAll('.CodeMirror'), function (el) {
		el.style.display = 'none';
	});
}

hintBtn.addEventListener("click", hintClicked, true);




// Performance measurement functions
var tictime;
if (!window.performance || !performance.now) { window.performance = { now: Date.now } }
function tic() { tictime = performance.now() }
function toc(msg) {
	var dt = performance.now() - tictime;
	console.log((msg || 'toc') + ": " + dt + "ms");
}

// Add syntax highlighting to the textarea
var editor = CodeMirror.fromTextArea(commandsElm, {
	mode: 'text/x-mysql',
	viewportMargin: Infinity,
	indentWithTabs: true,
	smartIndent: true,
	lineNumbers: true,
	matchBrackets: true,
	autofocus: false,
	lineWrapping: true,
	extraKeys: {
		"Ctrl-Enter": execEditorContents,
		//"Ctrl-S": savedb,
	}
});
