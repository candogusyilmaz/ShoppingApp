using ShoppingApp.Infrastructure.Abstract;

namespace ShoppingApp.Business.Concrete;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Create(User entity)
    {
        if (IsPasswordValid(entity.Password) is false)
            throw new ArgumentException("Password must be 8 characters long and contain uppercase, lowercase and digit.");

        await _userRepository.Add(entity);
    }

    public async Task<List<User>> GetAll()
    {
        return await _userRepository.GetAll();
    }

    public async Task<User> ValidateLogin(string email, string password)
    {
        if (email?.Length < 8 && password?.Length < 8)
            return null;

        return await _userRepository.Get(email, password);
    }

    private bool IsPasswordValid(string password)
    {
        int minLength = 8;
        bool containsUppercase = false;
        bool containsLowercase = false;
        bool containsDigit = false;
        bool greaterOrEqualToMinLength = password.Length >= minLength;

        foreach (char c in password)
        {
            if (char.IsUpper(c))
            {
                containsUppercase = true;
                continue;
            }
            if (char.IsLower(c))
            {
                containsLowercase = true;
                continue;
            }
            if (char.IsDigit(c))
            {
                containsDigit = true;
                continue;
            }
        }

        return containsUppercase && containsLowercase && containsDigit && greaterOrEqualToMinLength;
    }
}
