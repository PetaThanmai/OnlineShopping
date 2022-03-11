using Postdb.Models;
using Dapper;
using Postdb.Utilities;
using Postdb.DTOs;

namespace Postdb.Repositories;


public interface ITagsRepository
{
    Task<Tags> Create(Tags Item);
    Task<bool> Update(Tags item);
    Task<bool> Delete(long TagsId);
    Task<Tags> GetById(long TagsId);
    Task<List<Tags>> GetList();
    Task<List<TagsDTO>> GetList(object tagsId);
}
public class TagsRepository : BaseRepository, ITagsRepository
{
    public TagsRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Tags> Create(Tags item)
    {


        var query = $@"INSERT INTO ""{TableNames.tags}""
        (tag_id,product_id,title,colour,tag_size)
        VALUES (@TagId,  @ProductId, @Title, @Colour, @TagSize) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Tags>(query, item);

            return res;
        }

    }

    public async Task<bool> Delete(long TagsId)
    {
        var query = $@"DELETE FROM ""{TableNames.tags}""
        WHERE TagsId = @TagsId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { TagsId });
            return res > 0;
        }
    }

    public async Task<Tags> GetById(long TagsId)
    {
        var query = $@"SELECT * FROM ""{TableNames.tags}""
        WHERE Tags_id = @TagsId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Tags>(query, new
            {
                Tagsid = TagsId
            });

    }

    public async Task<List<Tags>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.tags}""";
        List<Tags> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Tags>(query)).AsList();
        return res;
    }

    public Task<List<TagsDTO>> GetList(object tagsId)
    {
        
        return null;
    }

    public async Task<bool> Update(Tags item)
    {
        var query = $@"UPDATE ""{TableNames.tags}"" SET 
         Title=@Title,Colour=@Colour,Tag_size=@TagSize WHERE TagsId = @TagsId";


        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;

        }
    }
}