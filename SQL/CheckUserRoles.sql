SELECT ur.UserId, ur.RoleId, r.Name AS RoleName, u.Email
FROM AspNetUserRoles ur
JOIN AspNetRoles r ON ur.RoleId = r.Id
JOIN AspNetUsers u ON ur.UserId = u.Id
WHERE u.Email = 'danaerougkeri@outlook.com';



