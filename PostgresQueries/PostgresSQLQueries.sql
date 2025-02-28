-- 1. Find the top 5 students with the highest GPA
SELECT
    s.StudentName,
    AVG(
        CASE
            WHEN g.Grade = 'A' THEN 4.0
            WHEN g.Grade = 'B' THEN 3.0
            WHEN g.Grade = 'C' THEN 2.0
            WHEN g.Grade = 'D' THEN 1.0
            WHEN g.Grade = 'F' THEN 0.0
            ELSE NULL
        END
    ) AS GPA
FROM
    Students s
    JOIN Grades g ON s.StudentID = g.StudentID
GROUP BY
    s.StudentID,
    s.StudentName
ORDER BY
    GPA DESC
LIMIT
    5;

-- 2. Calculate the average grade for each course
SELECT
    c.CourseName,
    AVG(
        CASE
            WHEN g.Grade = 'A' THEN 4.0
            WHEN g.Grade = 'B' THEN 3.0
            WHEN g.Grade = 'C' THEN 2.0
            WHEN g.Grade = 'D' THEN 1.0
            WHEN g.Grade = 'F' THEN 0.0
            ELSE NULL
        END
    ) AS AverageGrade
FROM
    Grades g
    JOIN Courses c ON g.CourseID = c.CourseID
GROUP BY
    c.CourseName
ORDER BY
    c.CourseName;

-- 3. Count the number of students enrolled in each major
SELECT
    Major,
    COUNT(*) AS NumberOfStudents
FROM
    Students
GROUP BY
    Major
ORDER BY
    Major;

-- 4. Identify the courses with the highest student enrollment
SELECT
    c.CourseName,
    COUNT(e.StudentID) AS NumberOfStudents
FROM
    Enrollments e
    JOIN Courses c ON e.CourseID = c.CourseID
GROUP BY
    c.CourseName
ORDER BY
    NumberOfStudents DESC;

-- 5. Calculate the student retention rate
SELECT
    100.0 * COUNT(
        CASE
            WHEN EnrollmentDate < DATE_TRUNC ('year', CURRENT_DATE - INTERVAL '1 year') THEN 1
        END
    ) / COUNT(*) AS RetentionRate
FROM
    Students;

-- 6. Find the professors teaching the most courses
Select
    ProfessorName,
    count(CourseID)
from
    Professors p
    join Courses c on p.Department = c.Department
group by
    ProfessorName
order by
    count(CourseID) Desc

    -- 7. List students who have failed more than one course
SELECT
    s.StudentName,
    COUNT(g.Grade) AS NumberOfFailures
FROM
    Students s
    JOIN Grades g ON s.StudentID = g.StudentID
WHERE
    g.Grade = 'F'
GROUP BY
    s.StudentName
HAVING
    COUNT(g.Grade) > 1
ORDER BY
    NumberOfFailures DESC;

-- 8. Analyze semester-wise student performance trends
SELECT
    g.Semester,
    AVG(
        CASE
            WHEN g.Grade = 'A' THEN 4.0
            WHEN g.Grade = 'B' THEN 3.0
            WHEN g.Grade = 'C' THEN 2.0
            WHEN g.Grade = 'D' THEN 1.0
            WHEN g.Grade = 'F' THEN 0.0
            ELSE NULL
        END
    ) AS AverageGrade
FROM
    Grades g
GROUP BY
    g.Semester
ORDER BY
    g.Semester;

-- 9. Calculate the percentage of students passing each course
SELECT
    c.CourseName,
    100.0 * SUM(
        CASE
            WHEN g.Grade IN ('A', 'B', 'C') THEN 1
            ELSE 0
        END
    ) / COUNT(g.Grade) AS PassingPercentage
FROM
    Courses c
    JOIN Grades g ON c.CourseID = g.CourseID
GROUP BY
    c.CourseName
ORDER BY
    PassingPercentage DESC;

-- 10. Find students who changed their major after enrollment
SELECT
    s.StudentName,
    s.Major
FROM
    Students s
WHERE
    s.StudentID IN (
        SELECT
            StudentID
        FROM
            Students
        GROUP BY
            StudentID
        HAVING
            COUNT(DISTINCT Major) > 1
    )
ORDER BY
    s.StudentName;

-- 11. Determine the course completion rate
SELECT
    c.CourseName,
    100.0 * SUM(
        CASE
            WHEN g.Grade IS NOT NULL THEN 1
            ELSE 0
        END
    ) / COUNT(e.StudentID) AS CompletionRate
FROM
    Courses c
    JOIN Enrollments e ON c.CourseID = e.CourseID
    LEFT JOIN Grades g ON e.StudentID = g.StudentID
    AND e.CourseID = g.CourseID
GROUP BY
    c.CourseName
ORDER BY
    CompletionRate DESC;

-- 12. Identify professors whose students have the highest average grades
SELECT
    p.ProfessorName,
    AVG(
        CASE
            WHEN g.Grade = 'A' THEN 4.0
            WHEN g.Grade = 'B' THEN 3.0
            WHEN g.Grade = 'C' THEN 2.0
            WHEN g.Grade = 'D' THEN 1.0
            WHEN g.Grade = 'F' THEN 0.0
            ELSE NULL
        END
    ) AS AverageGrade
FROM
    Professors p
    JOIN Departments d ON p.ProfessorName = d.Dean
    JOIN Courses c ON d.DepartmentName = c.Department
    JOIN Grades g ON c.CourseID = g.CourseID
GROUP BY
    p.ProfessorName
ORDER BY
    AverageGrade DESC;

-- 13. Calculate the attendance rate for each student
SELECT
    s.StudentName,
    100.0 * SUM(
        CASE
            WHEN a.Status = 'Present' THEN 1
            ELSE 0
        END
    ) / COUNT(a.Status) AS AttendanceRate
FROM
    Students s
    JOIN Attendance a ON s.StudentID = a.StudentID
GROUP BY
    s.StudentName
ORDER BY
    AttendanceRate DESC;

-- 14. Identify the most frequently skipped courses
SELECT
    c.CourseName,
    100.0 * SUM(
        CASE
            WHEN a.Status = 'Absent' THEN 1
            ELSE 0
        END
    ) / COUNT(a.Status) AS AbsenteeRate
FROM
    Courses c
    JOIN Attendance a ON c.CourseID = a.CourseID
GROUP BY
    c.CourseName
ORDER BY
    AbsenteeRate DESC;

-- 15. Find the department with the highest student enrollment
SELECT
    d.DepartmentName,
    COUNT(s.StudentID) AS NumberOfStudents
FROM
    Departments d
    JOIN Courses c ON d.DepartmentName = c.Department
    JOIN Enrollments e ON c.CourseID = e.CourseID
    JOIN Students s ON e.StudentID = s.StudentID
GROUP BY
    d.DepartmentName
ORDER BY
    NumberOfStudents DESC;