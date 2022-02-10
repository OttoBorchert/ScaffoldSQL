var execBtn = document.getElementById("execute");
var outputElm = document.getElementById('output');
var errorElm = document.getElementById('error');
var commandsElm = document.getElementById('commands');
var dbFileElm = document.getElementById('dbfile');
var savedbElm = document.getElementById('savedb');
var testCaseElm = document.getElementById('testcase_output')
var hintBtn = document.getElementById('hint')
var hintAreaElm = document.getElementById('hint_area')
var questionString = document.getElementById('questionOutput')
var nohintClass = document.getElementsByClassName('nohint')
var reshuffleBtn = document.getElementById('newInstanceLink')
var hintExecuteBtn = document.getElementById('feedbackLink')

var worker = new Worker("../../dist/worker.sql-wasm-debug.js");
worker.onerror = error;

var attempts = null;
var hintCalled = false;
var questionOutput = "";
var userAnswer = "";

var writeExecTime = "";
var hintExecuteTimestamp = "";
var questionData = {}
var parsonProblem = '';
var db_loaded = false;

//import fs from 'fs';

//fs.readFile('testLine.txt', 'utf8', function (err, data) {
//	if (err) throw err;
//	console.log(data);
//});

//// Open a database
//worker.postMessage({ action: 'open' });

// Connect to the HTML element we 'print' to
function print(text) {
    outputElm.innerHTML = text.replace(/\n/g, '<br>');
}

function loadBookDB(database = "fantasy.db") {
    $.ajax({
        type: 'get',
        url: `FileController/Database/Bytes/${database}`,
        success: function (ev) {
            worker.onmessage = ev => {
                console.log("Database loaded!");
                db_loaded = true;
            }
            worker.postMessage({
                action: "open",
                buffer: ev
            });
            worker.postMessage({
                action: 'exec',
                sql: "SELECT name FROM sqlite_master;"
            });
        }
    });
}

function error(e) {
    console.log(e);
    errorElm.style.height = '2em';
    errorElm.textContent = e.message;
    errorCalled = true;
}

function noerror() {
    errorElm.style.height = '2em';
    errorElm.textContent = " ";
}

// Run a command in the database
function execute(commands, shallRunAsserts = true) {
    if (!db_loaded) {
        console.log("Database is not yet loaded. Aborting call.")
        return;
    }
    commands = DesanitizeHTMLParsons(commands);
    testCaseElm.innerHTML = ""

    // If the text area for the free-form typing interface is not visible, then hint has been called
    hintCalled = !($("#hint_area").is(":hidden"))

    if (typeof(firebase) !== 'undefined') {
        writeExecTime = new Date(firebase.firestore.Timestamp.now().seconds * 1000).toLocaleString('en-US', 'best-fit', 'short');
    } else {
        console.log('Firebase: undefined');
        writeExecTime = new Date().toLocaleString('en-US', 'best-fit', 'short');
    }

    var username = document.getElementsByClassName("fs-block");
    if (username) {
        console.log(username)
        console.log("USERNAME: " + username)
    }
    noerror();
    tic();
    worker.onmessage = function (event) {
        if (event.data.error) {
            event.message = event.data.error;
            error(event);
        }

        var results = event.data.results;
        toc("Executing SQL");

        tic();
        outputElm.innerHTML = "";
        if (results) {
            if (results.length === 0 && shallRunAsserts) {
                runAsserts('');
            } else {
                toExec = shallRunAsserts ?
                    function (row) { runAsserts(row); outputElm.appendChild(tableCreate(row.columns, row.values)); }
                    :
                    function (row) { outputElm.appendChild(tableCreate(row.columns, row.values)); };
                results.forEach(toExec);
            }
        }
        toc("Displaying results");
    }

    userAnswer = commands; // This variable is used by firebase to store what the user entered as their SQL statement
    worker.postMessage({ action: 'exec', sql: commands });
    outputElm.textContent = "Fetching results...";
}

var runAsserts = function (results) {
    const params = new URLSearchParams(window.location.search)
    if (params.has('q')) {
        question = params.get('q')
        question.replace(/./, _)
        checkAsserts(results, questionData.TestCases);
    }
}



//pass test case result from checkAsserts function to check if the string contains '100%'

var checkIncorrectAnswer = function (result, parsonInterface) {
    var questionO = window.questionOutput
    var newQuestionO = questionO.replace(/\r/, "");
    //var parsonInterface = window.

    //var newQuestionOutput = newQuestionO.replace(/"\"/g, "");
    var questionNumber = window.question;
    questionNumber.replace(/./, _)
    // console.log("THIS IS THE QUESTION " + questionNumber);

    //var writeExecTime = window.executeTimestamp;
    // console.log("The execute timestamp is " + writeExecTime);

    //var writeHintTime = window.hintExecuteTimestamp;
    // console.log("This hint timestamp is " + writeHintTime);

    // console.log("QuestionO is " + newQuestionO);
    const subString = "100%";

    //if result does not contain '100%', increment attempts and print to console
    if (!result.includes(subString)) {
        // console.log("Not a 100%, sorry!");
        attempts += 1;
        write_Hint_And_Execute_Timestamps_To_FireBase(hintCalled, attempts, newQuestionO, questionNumber, result);

        // console.log("You have answered a question incorrectly " + attempts + " times.");
    }
    //else result includes the substring of '100%' in results/answer is correct
    else {
        attempts += 1;
        console.log("You have answered a question correctly, it only took " + attempts + " times");

        //if the question is answered correctly the first time without clicking the hint button, you cannot continue to spam click 
        //the buttons that writes to the firebase
        $("#execute").hide();
        $("#hint").hide();
        $("#showTables").hide();
        $("#refresh").hide();


        //hint button was not clicked
        if (hintCalled === false && questionData.TextAreaEnable) {
            //write_Attempt_To_FireBase(attempts);
            write_Execute_Timestamp_To_FireBase(hintCalled, attempts, newQuestionO, questionNumber, result);
        } 
        //hint button was clicked and is answered correctly, hide reshuffle and execute button for hint area
        else {
            //write_Attempt_To_FireBase(attempts);
            $("#feedbackLink").addClass("invisible");
            $("#newInstanceLink").addClass("invisible");
            write_Hint_And_Execute_Timestamps_To_FireBase(hintCalled, attempts, newQuestionO, questionNumber, result);
            console.log("You hit hint button " + hintCalled);
        }

        attempts = 0;
    }

    /* 
    // Removing automatic hint forwarding for experiment

    //if attempts == 5 and the hint button was not clicked, click the hint button. Test case interface informs Parsons interface
    if (attempts == 5 && hintCalled == false && parsonInterface == false) {
        write_Hint_And_Execute_Timestamps_To_FireBase(hintCalled, attempts, newQuestionO, questionNumber);
        // console.log("You have answered a question incorrectly " + attempts + " times.");
        //attempts += 1;

        //attempts = 0;
    }
    else if (attempts == 5 && hintCalled == false) {
        document.getElementById('hint').click();
        //write_Hint_And_Execute_Timestamps_To_FireBase(hintCalled, attempts, newQuestionO, questionNumber);
        // console.log("You have answered a question incorrectly " + attempts + " times.");
    }
    */
}

String.prototype.hashCode = function () {
    var hash = 0, i, chr;
    if (this.length === 0) return hash;
    for (i = 0; i < this.length; i++) {
        chr = this.charCodeAt(i);
        hash = ((hash << 5) - hash) + chr;
        hash |= 0; // Convert to 32bit integer
    }
    return hash;
};

var write_Execute_Timestamp_To_FireBase = function (called, attempts, questionOutput, questionNumber, result) {

    var executeTime = window.writeExecTime;

    var allCookies = document.cookie;
    //Creating a hash out of the session cookie, to hopefully track an individual's attempts, at least a little bit
    var userHash = allCookies.hashCode();


    var parsonsData = {
        UserHash: userHash,
        ExecuteTimestamp: executeTime,
        Parson: called,
        Attempts: attempts,
        Question: questionOutput,
        QuestionNumber: questionNumber,
        StudentAnswer: userAnswer,
        TestCaseResult: $(".testcase_summary").html()
    }
    console.log(parsonsData);

    var string = "entry";

    //var ref = database.ref('parsons').child(string);
    //ref.push(parsonsData);
}

var write_Hint_And_Execute_Timestamps_To_FireBase = function (called, attempts, questionOutput, questionNumber, result) {

    //this.attempts = window.attempts
    // console.log("This is attempts in writeAttempts " + attempts);

    var executeTime = window.writeExecTime;
    var writeHintTime = window.hintExecuteTimestamp;

    var allCookies = document.cookie;
    //Creating a hash out of the session cookie, to hopefully track an individual's attempts, at least a little bit
    var userHash = allCookies.hashCode();


    //This data var, you fill out what you want to add to the database
    var attemptsData = {
        UserHash: userHash,
        ExecuteTimestamp: executeTime,
        HintTimestamp: writeHintTime,
        Parson: called,
        Attempts: attempts,
        Question: questionOutput,
        QuestionNumber: questionNumber,
        StudentAnswer: userAnswer,
        TestCaseResult: $(".testcase_summary").html()
    }
    console.log(attemptsData);


    var string = "entry";
    //var ref = database.ref('parsons').child(string);
    //ref.push(attemptsData);
}

//    //entryIncrement(attemptsData);


//    //this will add a child to qid, in this case '2_2', .set(data) is what writes the information to that destination
//    //database.ref('qid').child('2_2').child('attempts').set(attemptsData);
//    //var entry = 0;
//}

var runAssertsOverList = function (result_table, testList) {
    let result = '';
    for (let test of testList) {
        try {
            let wlist = test.split(/\s+/);
            // console.log(wlist)
            let assertType = wlist.shift();
            // TODO: shorten
            if (assertType === 'V') {
                let loc = wlist.shift();
                let oper = wlist.shift();
                let expected = wlist.join(" ").trim();
                let [row, col] = loc.split(",");
                result += this.testValueAssert(
                    row,
                    col,
                    oper,
                    expected,
                    result_table
                );
            } else if (assertType === 'C') {
                let loc = wlist.shift();
                let oper = wlist.shift();
                let expected = wlist.join(" ").trim();
                result += this.testColumnAssert(
                    loc,
                    oper,
                    expected,
                    result_table
                );
            } else if (assertType === 'L') {
                let length_type = wlist.shift();
                let expected = wlist.shift();
                result += this.testLengthAssert(
                    length_type,
                    expected,
                    result_table
                );
            }
        } catch (e) {
            this.failed += 1;
        }
        result += "\n";
    }
    return result;
}

var checkAsserts = function (result_table, test_list) {
    // console.log("parsons interface is " + parsonInterfaceInput);
    var parsonInterface = questionData.ParsonsEnable;
    // console.log("parsons interface is " + parsonInterface);

    //if parsonInterface is false, then the hint button/Parson problem is not available for the question 
    if (parsonInterface === false) {
        $("#execute").click(function () {
            $("#hint").addClass("invisible")
        });
    }
            
    testCaseInterface = questionData.TestCaseEnable;
    // If testcases are enabled
    if (testCaseInterface) {
        // Enable the functions
        addFailMessage = EnabledAddFailMessage;
        addPassMessage = EnabledAddPassMessage;
    } else {
        // Otherwise, disable them.
        addFailMessage = addPassMessage = function(message) { };
    }
    console.log("testCaseInterface is " + testCaseInterface);

    //this is where code_Word and ppcode_word read the text file
    // TODO: Replace with enhanced code word selection
    //this.code_Word = questionData.SecretWord;
    //this.ppCode_Word = questionData.SecretWordParson;

    this.passed = 0;
    this.failed = 0;
    // Tests should be of the form
    // assert row,col oper value for example
    // assert 4,4 == 3
    var result = runAssertsOverList(result_table, test_list);
    let sum = this.passed + this.failed;
    if (sum) {
        let pct = (100 * this.passed) / (this.passed + this.failed);
        pct = pct.toLocaleString(undefined, { maximumFractionDigits: 2 });
        result += `<div class="testcase_summary">You passed ${this.passed} out of ${this.passed + this.failed} tests for ${pct}%</div>`;
    } else {
        result += `<div class="testcase_summary">You passed 0 out of 0 tests for 100%</div>`;
    }

    function CodeSelector(code) {
        return code[new Date().getTime() % code.length];
    }

    let OnAjaxSuccess = function (result, code, parsonInterface) {
        code = Decryption(CodeSelector(code.split(', ')));
        testCaseElm.innerHTML += `<div class="testcase_summary">Codeword is ${code}</div>`
        console.log(`This is the code word: ${code}`);
        //checkIncorrectAnswer(result, parsonInterface);
    }

    // If the answer is correct
    if (this.failed === 0) {
        // Decrypt and output the code word with respect to if this is parsons or not
        $.ajax({
            type: 'POST',
            url: hintCalled ? 'FileController/SecretWord/Parsons' : 'FileController/SecretWord',
            //headers: { __RequestVerificationToken: $('[name="__RequestVerificationToken"]').val() },
            data: {
                //__RequestVerificationToken: $('[name="__RequestVerificationToken"]').val(),
                contentType: "application/txt; charset=utf-8", 
                dataType: "text", 
                File: questionData.File
            },
            success: ev => OnAjaxSuccess(result, ev, parsonInterface),
            //error: ev => alert(`An error has occured. Please try again later. More information: ${ev.responseText}`)
            error: ev => console.log(ev)
        })
    }

    testCaseElm.innerHTML += result

    checkIncorrectAnswer(result, parsonInterface);
}

//handles the decryption of either code_Word or ppCode_Word
function Decryption(code_word) {
    var decrypted = CryptoJS.AES.decrypt(code_word, "SQL is the best").toString(CryptoJS.enc.Utf8);
    return decrypted === '' ? code_word : decrypted;
    //console.log("Decrypted is " + decrypted);
}

/**
 * Prints the message as a success.
 * @param {any} message
 */
function EnabledAddPassMessage(message) {
    output = "<div class='pass'>" + message + "</div>"
    //Remove this to turn off testcase
    testCaseElm.innerHTML += output
}

/**
 * Prints the message as a fail.
 * @param {any} message
 */
function EnabledAddFailMessage(message) {
    output = "<div class='fail'>" + message + "</div>"
    //remove this to turn off textcase
    testCaseElm.innerHTML += output
}

// Outputs the pass message for all the test cases
var addPassMessage = EnabledAddPassMessage;

// Outputs the fail message for all the test cases
var addFailMessage = EnabledAddFailMessage;

var testLengthAssert = function (length_type, expected, result_table) {
    var actual;
    if (length_type === "R") {
        actual = result_table.values.length
    } else if (length_type === "C") {
        actual = result_table.columns.length
    }
    let output = "";
    //var trueBool = "true"
    //var testCaseInterfaceCheck = window.testCaseInterface;
    //console.log("testcaseInterface in testLengthAssert function " + testCaseInterfaceCheck);

    let res = expected == actual;
    if (res) {
        if (length_type === "R") {
            addPassMessage(`Pass: ${actual} == ${expected} for number of rows`);
            console.log("expected == actual and length_type == R && testCaseInterfaceCheck.includes(trueBool) pass message");
        } else if (length_type === "C") {
            addPassMessage(`Pass: ${actual} == ${expected} for number of columns`);
            console.log("expected == actual and length_type == C && testCaseInterfaceCheck.includes(trueBool) pass message");
        }
        this.passed++;
    } else {
        if (length_type === "R") {
            addFailMessage(`Fail: ${actual} == ${expected} for number of rows`);
            console.log("expected == actual and length_type == R && testCaseInterfaceCheck.includes(trueBool) fail message");
        } else if (length_type === "C") {
            addFailMessage(`Fail: ${actual} == ${expected} for number of columns`);
            console.log("expected == actual and length_type == c && testCaseInterfaceCheck.includes(trueBool) fail message");
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


var testColumnAssert = function (loc, oper, expected, result_table) {
    let output = "";

    /*var testCaseInterfaceCheck = window.testCaseInterface;
    var trueBool = "true";*/

    try {
        let actual = result_table.columns[loc];
        console.log(actual == expected);
        console.log("'" + actual + "'");
        console.log("'" + expected + "'");
        let res = operators[oper](actual, expected);
        if (res) {
            addPassMessage(`Pass: ${actual} ${oper} ${expected} for column name in column ${loc}`);
            this.passed++;
        } else {
            addFailMessage(`Fail: ${actual} ${oper} ${expected} for column name in column ${loc}`);
            this.failed++;
        }
    } catch {
        console.log("Fail: Missing row/column");
        addFailMessage(`Fail: Expected Row, Column ${row}, ${col} to exist. This was not found to be true.`);
        this.failed++;
    }

    return output;
}


var testValueAssert = function (row, col, oper, expected, result_table) {
    /*var testCaseInterfaceCheck = window.testCaseInterface;
    console.log("testcaseInterface in testValueAssert function " + testCaseInterface);
    var trueBool = "true";*/
    let output = "";

    try {
        let actual = result_table.values[row][col];
        let res = operators[oper](actual, expected);
        if (res) {
            addPassMessage(`Pass: ${actual} ${oper} ${expected} in row ${row} column ${result_table.columns[col]}`);
            this.passed++;
        } else {
            addFailMessage(`Fail: ${actual} ${oper} ${expected} in row ${row} column ${result_table.columns[col]}`);
            this.failed++;
        }
    } catch {
        addFailMessage(`Fail: Expected Row, Column ${row}, ${col} to exist. This was not found to be true.`);
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
    noerror();
    clearAllOutputs();
    execute(editor.getValue() + ';');
}
execBtn.addEventListener("click", execEditorContents, true);

// Execute the commands when the button is clicked
function hintClicked() {
    hintCalled = true;

    //takes the timestamp when the hint button is clicked
    var hintTimestamp = Date.now();
    hintExecuteTimestamp = new Date(hintTimestamp);

    //assigns the date and time to hintExecuteTimestamp
    if (typeof(firebase) !== 'undefined') {
        hintExecuteTimestamp = new Date(firebase.firestore.Timestamp.now().seconds * 1000).toLocaleString('en-US', 'best-fit', 'short');
    } else {
        console.log('Firebase: undefined');
        hintExecuteTimestamp = new Date().toLocaleString('en-US', 'best-fit', 'short');
    }

    initializeParsonsProblem();
}

function initializeParsonsProblem() {
    var initial = parsonProblem;

    $(document).ready(function () {
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
        $("#newInstanceLink").click(function (event) {
            event.preventDefault();
            parson.shuffleLines();
        });
        $("#feedbackLink").click(function (event) {
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
    $("#noHintArea").hide();
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

var editor;
window.addEventListener('load', () => InitCodeMirror());

/**
 * Initialiazes the CodeMirror to its default settings.
 * @param {any} tableData
 * Must be of the following object layout:
 * {
 *  table1: [column1, column2, colum3, etc],
 *  table2: [column1, column2, colum3, etc],
 *  table3: [column1, column2, colum3, etc],
 *  etc
 * }
 */
function InitCodeMirror(tableData = null) {
    tableData = tableData ?? {};
    // Assign to global variable
    editor = CodeMirror.fromTextArea(commandsElm, {
        mode: 'text/x-sql',
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
        },
        hintOptions: {
            tables: tableData
        }
    });

    // Assign to window.editor (unsure if this actually does anything, but it is done in the official CodeMirror exmaple)
    window.editor = editor;
}

function clearNode(node) {
    node.innerHTML = '';
}

function clearAllOutputs() {
    clearNode(document.getElementById('database_info_output'));
    //clearNode(document.getElementById('questionOutputHint'));
    //clearNode(document.getElementById('questionOutputNoHint'));
    clearNode(document.getElementById('testcase_output'));
    clearNode(document.getElementById('output'));
}

function DisplayDatabaseInfo(id) {
    const sql = `
        SELECT sql.tbl_name,
            info.name as column_name, info.type, info.pk as primary_key
        FROM sqlite_master sql
        JOIN pragma_table_info(tbl_name) AS info
        WHERE sql.type = 'table'
        ORDER BY tbl_name, column_name;`;
    worker.onmessage = function (results) {
        let table_display = document.getElementById(id);
        function ConvertToObj(header, values, toIgnore = []) {
            let dictionary = {};
            header.forEach((element, index) => {
                if (!toIgnore.includes(element)) {
                    let arr = dictionary[element] = [];
                    for (value of values) {
                        arr.push(value[index]);
                    }
                }
            });
            return dictionary;
        }

        let data = results.data.results[0], values = data.values, db = {};
        data = ConvertToObj(data.columns, data.values);

        // Clean the data
        for (row of values) {
            let tbl_name = row[0];
            let data = [row[1], row[2].toLowerCase(), row[3] === 1 ? 'Primary Key' : ''];
            if (db[tbl_name]) {
                db[tbl_name].push(data)
            } else {
                db[tbl_name] = [data];
            }
        }

        function CreateRow(data_element, row_values) {
            let row = document.createElement('tr');
            for (item of row_values) {
                let data_html = document.createElement(data_element);
                data_html.innerText = item;
                row.append(data_html);
            }
            return row;
        }

        // Write the tables
        for (table in db) {
            let html_table = document.createElement('table');
            let table_header = CreateRow('th', ['Column Name', 'Data Type', '']);
            let caption = document.createElement('caption');
            caption.innerText = table;
            html_table.append(caption, table_header);
            db[table].map(value => html_table.append(CreateRow('td', value)));
            table_display.appendChild(html_table);
        }
    }
    worker.postMessage({ action: 'exec', sql: sql });
}