# T2 Lifestyle Checker
An ASP.NET Core MVC lifestyle questionnaire application

## Running the application
Open the project in Visual Studio and run it
Make sure the API subscription key is set up in User Secrets.

The SQLite database will be created automatically when the application starts.

## Using the application
Enter the patient's NHS number, surname and date of birth.
If the details are correct and the patient is 16 or over, they can complete the questionnaire.

The questionaire asks about drinking, smoking and exercise.
The answers are scored based on the patients age. The result is then shown on the results page.

Patients under 16 are not eligible.

## Scoring
The scoring rules are stored in the SQLite database and can be accessed and changed via a sqlite3 editor.

A score of 3 or below gives the lower result. A score of 4 or above recommends booking an appointment.
