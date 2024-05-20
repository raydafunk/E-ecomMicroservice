using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService
        (DiscountContext discountDbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var couponObject = await discountDbContext
                 .Coupons
                 .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (couponObject is null)
                couponObject = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
            logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount : {amount}", couponObject.ProductName, couponObject.Amount);

            var couponModel = couponObject.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
           var couponobj = request.Coupon.Adapt<Coupon>();
            if (couponobj is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));

            discountDbContext.Add(couponobj);
            await discountDbContext.SaveChangesAsync();

            logger.LogInformation("Discount is sucessfull created. ProductName: {ProductName}", couponobj.ProductName);
            var couponModel = couponobj.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var couponobj = request.Coupon.Adapt<Coupon>();
            if (couponobj is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));

            discountDbContext.Update(couponobj);
            await discountDbContext.SaveChangesAsync();

            logger.LogInformation("Discount is sucessfull updated. ProductName: {ProductName}", couponobj.ProductName);
            var couponModel = couponobj.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
           var coupon = await discountDbContext
            .Coupons
            .FirstOrDefaultAsync(x=> x.ProductName == request.ProductName);

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName= {request.ProductName} is not found"));

            discountDbContext.Coupons .Remove(coupon);
            await discountDbContext.SaveChangesAsync();

            logger.LogInformation("Discount is sucessfull deleted. ProductName: {ProductName}", request.ProductName);
            return new DeleteDiscountResponse { Success= true };


        }
    }
}
