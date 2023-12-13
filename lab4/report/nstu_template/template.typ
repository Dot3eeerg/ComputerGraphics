#let project(
  faculty: none, // ФПМИ
  students: (),
  teachers: (),
  department: none,
  discipline: none,
  task_type: none,
  task_name: none,
  variant: none,
  group: none,
  team: none,
  year: none,
  body,
) = {
  // Set the document's basic properties.
  set document(author: students, title: task_name)
  set text(font: ("Raleway", "Fira Code"), lang: "ru", weight: "regular")

  page(
    
    background: image("images/border.png", width: 90%),
    align(center + horizon)[
   
    #set text(size:14pt, font: ("Raleway"))

    // Information about work
    #align(center + top)[
      #image("images/eagle.png", width: 20%)
  
      #v(3em, weak: true)
      
      Федеральное государственное бюджетное \
      образовательное учреждение высшего образования \
      #smallcaps(["Новосибирский государственный технический университет"]) \
  
      #v(3em, weak: true)

      #if {faculty == "ФПМИ"} [#image("images/logo_fami.png")]
  
      #v(3em, weak: true)
      
      #department
  
      #v(1.5em, weak: true)
  
      #if {task_type != none} [
        #task_type \
        #if {discipline != none } [ по дисциплине "#discipline" ]
      ] else if {discipline != none } [
        Дисциплина "#discipline"
      ]
  
      #v(1.5em, weak: true)
  
      #if {task_name != none} [ #text(weight: "semibold", smallcaps(task_name)) ]
    ]

    // Information about students
    #grid(
      columns: (5cm, 1fr),
      align(left + top)[
        #if {group != none} [ Группа: #group \ ]
        #if {team != none} [ Бригада: #team \ ]
        #if {variant != none} [ Вариант: #variant \ ]
      ],
      align(left + top)[
        #for student in students [ #upper(student) \ ]
      ],
    )

    // Information about teachers
    #grid(
      columns: (5cm, 1fr),
      align(left + top)[
        #if {teachers.len() != 0} [ Преподаватели: \ ]
      ],
      align(left + top)[
        #for teacher in teachers [#upper(teacher) \ ]
      ],
    )
    
    #align(center + bottom)[
      #if {year != none} [Новосибирск, #year] else [Новосибирск]
    ]
  ])
  
  // Main body.
  set page(numbering: "1", number-align: center)
  set par(justify: true)

  body
}